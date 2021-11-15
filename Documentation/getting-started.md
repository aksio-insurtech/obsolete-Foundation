# Getting started

## Pre requisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [.NET Core 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node JS version 16](https://nodejs.org/)

## Install

The project comes with scaffolding templates.
Install it by simply doing the following from your terminal:

```shell
dotnet new -i Aksio.Templates
```

The templates are also supported through Visual Studio
by enabling [.NET CLI tooling](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/).

## Usage

Create a folder to holds your microservice and from within this folder you run:

```shell
dotnet new <template-name>
```

| Template | Description |
| -------- | ----------- |
| aksioms  | Aksio Microservice template with ASP.NET Core for .NET 6 |

### AKSIOMS - Aksio Microservice

The Aksio Microservice template comes with an optional Web. By adding the `--IncludeWeb` option to
the command line, you'll get a setup with a React frontend with WebPack ready to go.

The result will be the following

```shell
<your microservice folder>
└───Main
└───Web
```

The template will do a post action to restore all **NuGet packages** as well as **node modules** if you
opted in for the web option.

#### Running

Before running the microservice backend and frontend, we will need to run the Dolittle Runtime

```shell
docker run -d -p 50052:50052 -p 50053:50053 -p 27017:27017 dolittle/runtime:latest-development
```

> Note: If you're running with an ARM64 based computer, such as the Apple M based Macs, you'll need
> a different image; dolittle/runtime:latest-arm64-development.
> `docker run -d -p 50052:50052 -p 50053:50053 -p 27017:27017 dolittle/runtime:latest-arm64-development`

Then you can simply run the microservice backend by running the following from the `Main` folder:

```shell
dotnet run
```

...or

```shell
dotnet watch run
```

> Note: if you're using an IDE such as Visual Studio or Rider, you typically would run it from within the IDE.

If you opted in to include a Web frontend, you run this by running the following from the `Web` folder:

```shell
yarn start:dev
```

The backend will start on port **5000** while the frontend will be on **9000**. The frontend is configured to
proxy `/api` and `/graphql` to the backend, meaning that you can do relative paths on the same origin for your
API calls from the frontend code.

#### Static Code Analysis

The template comes pre-configured with the Aksio static code analysis rules described [here](https://github.com/aksio-system/Defaults).
You can find the rule-sets [here](https://github.com/aksio-system/Defaults/tree/main/Source/Defaults).
For the Web part, it also comes with a pre-configured `.eslintrc.js` file with a set of rules and also a `tsconfig.json` file
with a default setup.
