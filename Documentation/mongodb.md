# MongoDB

MongoDB is a managed resource. This means that its configuration is managed by the Foundation in code
and also in the [managed platform running @ Dolittle](https://dolittle.io/docs/platform/requirements/). In addition, MongoDB is also considered a
[multi-tenant](./tenancy.md). resource which means that there should be one configuration / database
per [tenant](./tenancy.md).

The Foundation is built with the Dolittle [resource system](https://dolittle.io/docs/platform/requirements/#1-your-application-must-use-the-resource-system) in mind,
it provides an abstraction for the [resources.json](https://dolittle.io/docs/reference/runtime/configuration/#resourcesjson) file.
For you as a developer, it has been made very simple by letting you work with the MongoDB C# Driver API only through
a pre-configured setup for the [IoC Container](./ioc.md).

Usage:

```csharp
[Route("/api/employees")]
public class EmployeesController : Controller
{
    readonly IMongoDatabase _database;

    public EmployeesController(IMongoDatabase database) => _database = database;

    [HttpGet]
    public Task<Employee> GetAllEmployees()
    {
        var collection = _database.GetCollection<Employee>();
        return collection.FindAsync(_ => true).ConfigureAwait(false);
    }
}
```

> Important: It is very important that you don't create a singleton and keep the instance around, as you will be handed the
> correct instance for the tenant in the current context.

The `GetCollection<Employee>()` call is an extension method provided by the Foundation that automatically by convention
pluralizes the name of the collection and also makes it **camelCase**.

If you need to control the name of the collection, you can use one of the overrides for this:

```csharp
[Route("/api/employees")]
public class EmployeesController : Controller
{
    readonly IMongoDatabase _database;

    public EmployeesController(IMongoDatabase database) => _database = database;

    [HttpGet]
    public Task<Employee> GetAllEmployees()
    {
        var collection = _database.GetCollection<Employee>("MyEmployees");
        return collection.FindAsync(_ => true).ConfigureAwait(false);
    }
}
```
