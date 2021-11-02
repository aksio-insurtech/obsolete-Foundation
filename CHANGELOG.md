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

