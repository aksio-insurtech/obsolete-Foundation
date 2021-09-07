using System.Reflection;
using Aksio.Dolittle.Handling;
using Aksio.Reflection;
using Aksio.Types;
using Dolittle.SDK.Events.Handling.Builder;

namespace Dolittle.SDK.Events.Handling
{
    public static class EventHandlersExtensions
    {
        /// <summary>
        /// Hook up event handlers by convention through discovery.
        /// </summary>
        /// <param name="clientBuilder">The Dolittle <see cref="ClientBuilder"/>.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery,</param>
        /// <returns><see cref="ClientBuilder"/> for continuation.</returns>
        public static ClientBuilder WithAutoDiscoveredEventHandlers(this ClientBuilder clientBuilder, ITypes types)
        {
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

                        eventHandlerMethodsBuilder = eventHandlerMethodsBuilder
                            .Handle(eventTypes[eventType], (@event, context) =>
                            {
                                var handlerInstance = Activator.CreateInstance(handler);

                                if (method.GetParameters().Length == 2)
                                {
                                    return method.Invoke(handlerInstance, new object[] { @event, context }) as Task;
                                }

                                return method.Invoke(handlerInstance, new object[] { @event }) as Task;
                            });
                    }
                }
            });

            return clientBuilder;
        }
    }
}