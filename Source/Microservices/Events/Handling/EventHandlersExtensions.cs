using Aksio.Events.Handling;
using Cratis.DependencyInversion;
using Cratis.Types;
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
        /// <param name="eventTypes">The <see cref="IEventTypes"/>.</param>
        /// <param name="serviceProviderProvider">Provider for providing <see cref="IServiceProvider"/>.</param>
        /// <returns><see cref="ClientBuilder"/> for continuation.</returns>
        public static ClientBuilder WithAutoDiscoveredEventHandlers(this ClientBuilder clientBuilder, IServiceCollection services, ITypes types, Aksio.Events.Types.IEventTypes eventTypes, ProviderFor<IServiceProvider> serviceProviderProvider)
        {
            var middlewares = new EventHandlerMiddlewares(types, serviceProviderProvider);
            services.AddSingleton<IEventHandlerMiddlewares>(middlewares);
            var eventHandlers = new EventHandlers(types, eventTypes, serviceProviderProvider);
            services.AddSingleton<IEventHandlers>(eventHandlers);

            clientBuilder.WithEventHandlers(_ =>
            {
                foreach (var eventHandler in eventHandlers.All)
                {
                    var eventHandlerBuilder = _.CreateEventHandler(eventHandler.Id);
                    if (eventHandler.Scope != ScopeId.Default)
                    {
                        eventHandlerBuilder = eventHandlerBuilder.InScope(eventHandler.Scope);
                    }

                    EventHandlerMethodsBuilder eventHandlerMethodsBuilder;
                    if (eventHandler.Partitioned) eventHandlerMethodsBuilder = eventHandlerBuilder.Partitioned();
                    else eventHandlerMethodsBuilder = eventHandlerBuilder.Unpartitioned();

                    foreach (var method in eventHandler.Methods)
                    {
                        eventHandlerMethodsBuilder = eventHandlerMethodsBuilder
                             .Handle(method.EventType, middlewares.CreateInvokerFor(method));
                    }
                }
            });

            return clientBuilder;
        }
    }
}