using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using Cratis.Changes;
using Cratis.Reflection;

namespace Aksio.Integration
{
    /// <summary>
    /// Extension methods for building on the <see cref="IImportBuilderFor{TModel, TExternalModel}"/>.
    /// </summary>
    public static class ImportBuilderExtensions
    {
        /// <summary>
        /// Filter down to when one of the properties defined changes.
        /// </summary>
        /// <param name="builder"><see cref="IImportBuilderFor{TModel, TExternalModel}"/> to build the filter for.</param>
        /// <param name="properties">Properties as expressions to look for changes on.</param>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model.</typeparam>
        /// <returns>Observable for chaining.</returns>
        public static IObservable<ImportContext<TModel, TExternalModel>> WithProperties<TModel, TExternalModel>(this IImportBuilderFor<TModel, TExternalModel> builder, params Expression<Func<TModel, object>>[] properties)
        {
            var propertyPaths = properties.Select(_ => _.GetPropertyInfo().Name);

            return builder.Where(_ =>
            {
                var changes = _.Changeset.Changes.Where(_ => _ is PropertiesChanged<TModel>).Select(_ => _ as PropertiesChanged<TModel>);
                return changes.Any(_ => _!.Differences.Any(_ => propertyPaths.Contains(_.MemberPath)));
            });
        }

        /// <summary>
        /// Append an event by automatically mapping property names matching from the model onto the event.
        /// </summary>
        /// <param name="context">Observable of the <see cref="ImportContext{TModel, TExternalModel}"/>.</param>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model.</typeparam>
        /// <typeparam name="TEvent">Type of event to append.</typeparam>
        /// <returns>Observable for chaining.</returns>
        public static IObservable<ImportContext<TModel, TExternalModel>> AppendEvent<TModel, TExternalModel, TEvent>(this IObservable<ImportContext<TModel, TExternalModel>> context)
        {
            context.Subscribe(_ =>
            {
                var constructors = typeof(TEvent).GetConstructors(BindingFlags.Instance | BindingFlags.Public).OrderByDescending(_ => _.GetParameters().Length).ToArray();
                var constructor = constructors[0];
                var parameters = constructor.GetParameters();
                var sourceProperties = typeof(TModel).GetProperties();

                foreach (var change in _.Changeset.Changes.Where(_ => _ is PropertiesChanged<TModel>).Select(_ => _ as PropertiesChanged<TModel>))
                {
                    var values = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var added = false;
                        var difference = change!.Differences.FirstOrDefault(_ => _.MemberPath.Equals(parameter.Name, StringComparison.InvariantCultureIgnoreCase));
                        if (difference is not null)
                        {
                            var property = Array.Find(sourceProperties, _ => _.Name == difference.MemberPath);
                            if (property is not null)
                            {
                                values.Add(property!.GetValue(change.State)!);
                                added = true;
                            }
                        }

                        if (!added)
                        {
                            values.Add(null!);
                        }
                    }
                    _.Events.Add(constructor.Invoke(values.ToArray()));
                }
            });

            return context;
        }

        /// <summary>
        /// Append an event through calling a callback that will be responsible for creating an instance of the event.
        /// </summary>
        /// <param name="context">Observable of the <see cref="ImportContext{TModel, TExternalModel}"/>.</param>
        /// <param name="creationCallback">Callback for creating the instance.</param>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model.</typeparam>
        /// <typeparam name="TEvent">Type of event to append.</typeparam>
        /// <returns>Observable for chaining.</returns>
        public static IObservable<ImportContext<TModel, TExternalModel>> AppendEvent<TModel, TExternalModel, TEvent>(this IObservable<ImportContext<TModel, TExternalModel>> context, Func<ImportContext<TModel, TExternalModel>, TEvent> creationCallback)
        {
            context.Subscribe(_ => _.Events.Add(creationCallback(_)!));
            return context;
        }
    }
}