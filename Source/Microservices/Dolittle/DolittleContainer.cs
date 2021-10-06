using Aksio.Execution;
using ExecutionContext = Dolittle.SDK.Execution.ExecutionContext;

namespace Aksio.Dolittle
{
    /// <summary>
    /// Represents an implementation of <see cref="global::Dolittle.SDK.DependencyInversion.IContainer"/>.
    /// </summary>
    public class DolittleContainer : global::Dolittle.SDK.DependencyInversion.IContainer
    {
        readonly IServiceProvider _serviceProvider;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DolittleContainer"/> class.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> to use.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> to use.</param>
        public DolittleContainer(IServiceProvider serviceProvider, IExecutionContextManager executionContextManager)
        {
            _serviceProvider = serviceProvider;
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc/>
        public object Get(Type service, ExecutionContext context)
        {
            _executionContextManager.SetCurrent(context);
            return _serviceProvider.GetService(service)!;
        }

        /// <inheritdoc/>
        public T Get<T>(ExecutionContext context)
            where T : class
        {
            _executionContextManager.SetCurrent(context);
            return (T)_serviceProvider.GetService(typeof(T))!;
        }
    }
}