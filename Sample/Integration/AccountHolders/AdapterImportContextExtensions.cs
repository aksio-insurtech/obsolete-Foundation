using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reactive.Linq;
using Aksio.Integration;
using Cratis.Changes;
using Cratis.Events.Projections;
using Cratis.Events.Projections;
using Cratis.Reflection;

namespace Integration.AccountHolders
{
    public static class AdapterImportContextExtensions
    {
        public static IObservable<ImportContext<TModel, TExternalModel>> WithProperties<TModel, TExternalModel>(this IImportBuilderFor<TModel, TExternalModel> builder, params Expression<Func<TModel, object>>[] properties)
        {
            var propertyPaths = properties.Select(_ => _.GetPropertyInfo().Name);

            return builder.Where(_ =>
            {
                var changes = _.Changeset.Changes.Where(_ => _ is PropertiesChanged).Select(_ => _ as PropertiesChanged);
                return changes.Any(_ => _!.Differences.Any(_ => propertyPaths.Contains(_.MemberPath)));
            });
        }

        public static IObservable<ImportContext<TModel, TExternalModel>> AppendEvent<TModel, TExternalModel, TEvent>(this IObservable<ImportContext<TModel, TExternalModel>> context)
        {
            context.Subscribe(_ =>
            {
                var t = typeof(TEvent);
                Console.WriteLine(t);
            });

            return context;
        }

        public static IObservable<ImportContext<TModel, TExternalModel>> AppendEvent<TModel, TExternalModel, TEvent>(this IObservable<ImportContext<TModel, TExternalModel>> context, Func<ImportContext<TModel, TExternalModel>, TEvent> creationCallback)
        {
            context.Subscribe(_ =>
            {
                creationCallback(_);
                var t = typeof(TEvent);
                Console.WriteLine(t);
            });

            return context;
        }

        public static Task Apply<TModel, TExternalModel>(this ImportContext<TModel, TExternalModel> context, TExternalModel instance, EventSourceId? eventSourceId = default)
        {
            Console.WriteLine("Hello : " + context + instance + eventSourceId);
            return Task.CompletedTask;
        }

        public static Task Apply<TModel, TExternalModel>(this ImportContext<TModel, TExternalModel> context, IEnumerable<TExternalModel> instances, Func<TExternalModel, EventSourceId>? eventSourceIdProvider = default)
        {
            Console.WriteLine("Hello : " + context + instances + eventSourceIdProvider);
            return Task.CompletedTask;
        }

        public static Task Apply<TModel, TExternalModel>(this ImportContext<TModel, TExternalModel> context, Changeset<TExternalModel> changeset)
        {
            Console.WriteLine("Hello : " + context + changeset);
            return Task.CompletedTask;
        }
    }
}