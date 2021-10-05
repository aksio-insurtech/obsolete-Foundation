namespace Sample
{
    /*
    Projections:

        [Projection("610bbd9c-4024-40db-8bd2-38ea1481904d)]
        public class MyProjection : IProjection
        {
            public void Define(IProjectionBuilder builder)
            {
                builder
                    .ProjectTo<MyModel>("<optional name>")
                    .From<SomeEvent>(_ => _
                        .Set(model => model.SomeProperty).To(@event => @event.SomeProperty))
                    .From<SomeOtherEvent>(_ => _
                        .Set(model => model.SomeOtherProperty).To(@event => @event.SomeOtherProperty))
                    .RemovedWith<SomeDeleteEvent>(_ => _
                        .UsingKey(@event => @event.Id)) // Default to using the EventSourceId as the key
                    .Join<ThirdEvent>(_ => _
                        .On(model => model.RelationProperty)
                        .UsingKey(@event => @event.Id) // Default to using the EventSourceId as the key
                        .Set(model => model.ThirdProperty).To(@event => @event.PropertyFromTheThirdEvent))
                    .Children<SomeChildModel>(_ => _
                        .IdentifiedBy(childModel => childModel.Id))
                        .StoredIn(childModel => childModel.Children)
                        .From<ChildAdded>(cb => cb
                            .UsingKey(@event => @event.Id) // Default to using the EventSourceId as the key
                            cb.Set(childMOdel => childModel.Property).To(@event => @event.Property))
                        .RemovedWith<ChildRemoved>(cb => cb
                            .UsingKey(@event => @event.Id)) // Default to using the EventSourceId as the key

            }
        }
    */
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAksio();
        }
    }
}
