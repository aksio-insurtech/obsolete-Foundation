using System.Reflection;
using Aksio.Events.Handling;
using Aksio.Reflection;
using Aksio.Types;
using Dolittle.SDK.Events.Handling.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dolittle.SDK.Events.Handling
{
    /// <summary>
    /// Provides a set of extensions for working with event handlers.
    /// </summary>
    public static class EventHandlersExtensions
    {
        /// <summary>
        /// Hook up event handlers by convention through discovery.
        /// </summary>
        /// <param name="clientBuilder">The Dolittle <see cref="ClientBuilder"/>.</param>
        /// <param name="services"><see cref="IServiceCollection"/> for registering services.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <returns><see cref="ClientBuilder"/> for continuation.</returns>
        public static ClientBuilder WithAutoDiscoveredEventHandlers(this ClientBuilder clientBuilder, IServiceCollection services, ITypes types)
        {
            var eventHandlers = new EventHandlers();
            services.AddSingleton<IEventHandlers>(eventHandlers);

            var handlers = types.All.Where(_ => _.HasAttribute<EventHandlerAttribute>());
            var eventTypes = types.All.Where(_ => _.HasAttribute<EventTypeAttribute>()).ToDictionary(
                _ => _,
                _ => _.GetCustomAttribute<EventTypeAttribute>()!.EventType.Id);

            clientBuilder.WithEventHandlers(_ =>
            {
                foreach (var handler in handlers)
                {
                    var methodsByEventTypeId = handler.GetHandleMethods(eventTypes);

                    var eventHandler = handler.GetCustomAttribute<EventHandlerAttribute>()!;

                    foreach ((var eventType, var method) in methodsByEventTypeId)
                    {
                        var eventHandlerBuilder = _.CreateEventHandler(eventHandler.Identifier);
                        if (eventHandler.Scope != ScopeId.Default)
                        {
                            eventHandlerBuilder = eventHandlerBuilder.InScope(eventHandler.Scope);
                        }

                        EventHandlerMethodsBuilder eventHandlerMethodsBuilder;
                        if (eventHandler.Partitioned) eventHandlerMethodsBuilder = eventHandlerBuilder.Partitioned();
                        else eventHandlerMethodsBuilder = eventHandlerBuilder.Unpartitioned();

                        var eventHandlerMethod = eventHandlers.Register(
                            eventHandler.Identifier,
                            eventType,
                            eventTypes[eventType],
                            handler,
                            method);

                        eventHandlerMethodsBuilder = eventHandlerMethodsBuilder
                            .Handle(eventTypes[eventType], (@event, context) => eventHandlerMethod.Invoke(@event, context));
                    }
                }
            });

            return clientBuilder;
        }
    }
}