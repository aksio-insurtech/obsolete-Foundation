# [v1.12.3] - 2021-11-28 [PR: #46](https://github.com/aksio-system/Foundation/pull/46)

### Fixed

- String properties does not become `string[]` in the proxy generated TypeScript files.
- Adding support for arrays and leveraging the element type in the Roslyn API.


# [v1.12.2] - 2021-11-27 [PR: #44](https://github.com/aksio-system/Foundation/pull/44)

### Fixed

- Cleaning up the known types support for the proxy generator - leverage existing code for discovering, remove the custom code.

# [v1.12.1] - 2021-11-27 [PR: #43](https://github.com/aksio-system/Foundation/pull/43)

### Fixed

- Output correct property type for known types when generating types.

# [v1.12.0] - 2021-11-27 [PR: #42](https://github.com/aksio-system/Foundation/pull/42)

### Added

- ProxyGenerator now recognizes some well known "system" types and does not create new types for these but maps them to known JavaScript types.

# [v1.10.2] - 2021-11-25 [PR: #40](https://github.com/aksio-system/Foundation/pull/40)

### Fixed

- WebSockets will now retry with a backoff strategy if connection is either errored or closed unexpectedly.
- Cleaning up query subscriptions when a query gets out of React rendering scope.


# [v1.10.1] - 2021-11-24 [PR: #39](https://github.com/aksio-system/Foundation/pull/39)

### Fixed

- Fixed API alias configuration for TypeScript for Sample + Template - moved into the compiler options + fixed casing.


# [v1.10.0] - 2021-11-24 [PR: #38](https://github.com/aksio-system/Foundation/pull/38)

## Summary

Introducing reactive APIs. Observable from the client. This is a full pipeline support with proxygeneration, frontend support and all. 

Basically, add a controller action that leverages the new MongoCollection extension method:

```csharp
[HttpGet]
public Task<ClientObservable<IEnumerable<DebitAccount>>> AllAccounts()
{
      return _accountsCollection.Observe();
}
```

In the frontend, the generated proxy will now leverage the `ObservableQueryFor` as base class and adds the static convenience method on the query itself letting you do this:

```typescript
const [accounts] = AllAccounts.use();
```

Any changes will cause a rerender.

### Added

- Reactive/Observable APIs.


# [v1.9.0] - 2021-11-21 [PR: #37](https://github.com/aksio-system/Foundation/pull/37)

### Added

- Query actions (HttpGet) can now have return a single item. Proxies are generated accordingly and `IQueryFor` in the frontend supports this fully.

### Fixed

- When arguments for queries are not given but the query requires it, it will not execute the query.

# [v1.8.2] - 2021-11-17 [PR: #36](https://github.com/aksio-system/Foundation/pull/36)

### Fixed

- Adding automated tests for all of integration.
- Refactoring as a consequence of adding tests.
- Fixing minor issues as tests failed.

# [v1.8.1] - 2021-11-15 [PR: #35](https://github.com/aksio-system/Foundation/pull/35)

### Fixed

- Fixing AutoMapper bugs so that we can map back to `record` types. Reflecting this in docs and samples.

# [v1.8.0] - 2021-11-12 [PR: #34](https://github.com/aksio-system/Foundation/pull/34)

### Added

- Introduces an API for working with integration. Read more in our documentation on how it works.
- Supporting async query actions for the proxy generator.
- Improved error handling and messages from the proxy generator if expected type return is wrong.

### Fixed

- Crash bug in proxy generator for unknown types.

# [v1.7.7] - 2021-11-2 [PR: #32](https://github.com/aksio-system/Foundation/pull/32)

### Fixed

- Excluding `node_modules` from template packaging.

# [v1.7.6] - 2021-11-2 [PR: #31](https://github.com/aksio-system/Foundation/pull/31)

### Fixed

- Fixing publish pipeline for NuGet packages. Moving NoDefaultExclude option into .csproj for our template project where it belongs.


# [v1.7.5] - 2021-11-2 [PR: #29](https://github.com/aksio-system/Foundation/pull/29)

### Fixed

- Template packaging now excludes node_modules from Sample but includes files & directories starting with a . (dot)


# [v1.7.4] - 2021-11-2 [PR: #28](https://github.com/aksio-system/Foundation/pull/28)

## Fixed

- Template.json for DotNet template included again after some changes.

# [v1.7.3] - 2021-11-2 [PR: #26](https://github.com/aksio-system/Foundation/pull/26)

### Fixed

- The NuGet template updates to the latest NPM packages post creation.

# [v1.7.2] - 2021-11-1 [PR: #25](https://github.com/aksio-system/Foundation/pull/25)

### Fixed

- Rearranging the order of publishing to have NPM packages first, due to it updating references in the template used by the published `dotnet` template.


# [v1.7.1] - 2021-11-1 [PR: #24](https://github.com/aksio-system/Foundation/pull/24)

### Fixed

- Missing reference to `@aksio/frontend` package for template.
- Exporting deeper "namespaces" in `@aksio/frontend` package.


# [v1.7.0] - 2021-11-1 [PR: #23](https://github.com/aksio-system/Foundation/pull/23)

### Added

- Frontend representations of Commands and CommandResult.
- Frontend representations of Queries and QueryResult.

### Changed

- Proxy generation to follow the new frontend API

### Fixed

- Sample is now source for the template - easier to maintain one rather than two.


# [v1.6.3] - 2021-10-29 [PR: #22](https://github.com/aksio-system/Foundation/pull/22)

### Fixed

- When creating a new project without `--IncludeWeb` - it will now not have the reference to the `Aksio.ProxyGenerator` package and thus not create proxies for web purpose.


# [v1.6.2] - 2021-10-28 [PR: #21](https://github.com/aksio-system/Foundation/pull/21)

### Fixed

- Adding Main project to template solution file.
- Setting rootnamespaces and assembly names for projects in template.
- Centralizing package references for Aksio packages in Directory.Build.props
- Custom post action script that updates Aksio package references to latest.


# [v1.6.1] - 2021-10-28 [PR: #20](https://github.com/aksio-system/Foundation/pull/20)

### Fixed

- ProxyGenerator failed due to missing 3rd party assemblies in the analyzer folder of the package. These are now added.


# [v1.6.0] - 2021-10-27 [PR: #19](https://github.com/aksio-system/Foundation/pull/19)

### Added

- ProxyGenerator NuGet package that generates Command & Query proxy objects based on ASP.NET Controllers and their input and outputs.
- Scaffolded frontend package to match the requirements of the generated proxy objects.


# [v1.5.4] - 2021-10-25 [PR: #18](https://github.com/aksio-system/Foundation/pull/18)

### Changed

- Upgraded to Cratis 2.10.0 with improved projection engine and capabilities.
- Making use of the new capabilies in the sample and templates.

### Fixed

- Template was referencing wrong projects - fixed.


# [v1.5.3] - 2021-10-14 [PR: #17](https://github.com/aksio-system/Foundation/pull/17)

### Fixed

- Adding links to runtime debugging tools in template.
- Adding vscode setup for debugging in template.

# [v1.5.2] - 2021-10-14 [PR: #16](https://github.com/aksio-system/Foundation/pull/16)

### Fixed

- Dolittle ports exposed in docker compose file created by template.
- Added deposit sample into template.

# [v1.5.1] - 2021-10-14 [PR: #15](https://github.com/aksio-system/Foundation/pull/15)

### Fixed

- Upgrading to latest Cratis.

# [v1.5.0] - 2021-10-14 [PR: #14](https://github.com/aksio-system/Foundation/pull/14)

### Added

- Leveraging Cratis Projections one can now create projections from events to read models.
- Added a full end to end sample for the .NET tooling template.
- Adding Swagger default setup.
- JSON Converters hooked up for Dolittle - enabling us to use concepts on events
- JSON Converters hooked up for ASP.NET Core - enabling us to use concepts on models going in and coming out ('Commands' & Read Models).
- Adding the capability of taking `IMongoCollection<>` directly as a dependency.

# [v1.4.0] - 2021-10-6 [PR: #13](https://github.com/aksio-system/Foundation/pull/13)

### Added

- Adding support for the Dolittle resource system with multi tenancy support.
- Hooking up `IMongoDatabase` to use the resource system.
- Introducing convenience extension method for getting a MongoDB collection and its name resolved from the type, automatically pluralized and camelCased.


# [v1.3.0] - 2021-10-5 [PR: #12](https://github.com/aksio-system/Foundation/pull/12)

### Added

- Controllers from project referenced assemblies are now automatically discovered and hooked up.

# [v1.2.0] - 2021-10-4 [PR: #11](https://github.com/aksio-system/Foundation/pull/11)

### Added

- Adding frontend template to a Microservice.

# [v1.1.1] - 2021-9-20 [PR: #9](https://github.com/aksio-system/Foundation/pull/9)

### Fixed

- All Aksio assemblies are now included in type discovery.


# [v1.1.0] - 2021-9-20 [PR: #8](https://github.com/aksio-system/Foundation/pull/8)

### Added

- Introducing `Aksio.Templates` a `dotnet new` type of template pack. Installed by doing; `dotnet new -i Aksio.Templates`. Also supported by Visual Studio 20xx and Visual Studio for Mac.


# [v1.0.0] - 2021-9-15 [PR: #3](https://github.com/aksio-system/Foundation/pull/3)

Initial release.

# [v2.0.0] - 2021-9-15 [PR: #2](https://github.com/aksio-system/Foundation/pull/2)

Initial release.

# [v1.0.0] - 2021-9-15 [PR: #1](https://github.com/aksio-system/Foundation/pull/1)

Initial release.

