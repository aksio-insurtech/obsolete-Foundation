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

