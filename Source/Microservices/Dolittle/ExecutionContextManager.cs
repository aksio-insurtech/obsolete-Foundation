using System.Globalization;
using Dolittle.SDK.Execution;
using Dolittle.SDK.Security;
using Dolittle.SDK.Tenancy;
using Environment = Dolittle.SDK.Microservices.Environment;
using ExecutionContext = Dolittle.SDK.Execution.ExecutionContext;

namespace Aksio.Microservices.Dolittle
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionContextManager"/>.
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        static readonly AsyncLocal<ExecutionContext> _currentExecutionContext = new();

        /// <inheritdoc/>
        public ExecutionContext Current
        {
            get
            {
                ExecutionContextNotSet.ThrowIfNotSet(_currentExecutionContext.Value);
                return _currentExecutionContext.Value!;
            }
        }

        /// <inheritdoc/>
        public void SetCurrent(ExecutionContext current)
        {
            _currentExecutionContext.Value = current;
        }

        /// <inheritdoc/>
        public ExecutionContext Establish(TenantId tenantId, CorrelationId correlationId)
        {
            _currentExecutionContext.Value = new ExecutionContext(
                Guid.Empty,
                tenantId,
                new global::Dolittle.SDK.Microservices.Version(1, 0, 0, 0),
                RuntimeEnvironment.IsDevelopment ? Environment.Development : Environment.Production,
                correlationId,
                Claims.Empty,
                CultureInfo.InvariantCulture);

            return _currentExecutionContext.Value;
        }

        /// <inheritdoc/>
        public void Set(ExecutionContext context) => _currentExecutionContext.Value = context;
    }
}