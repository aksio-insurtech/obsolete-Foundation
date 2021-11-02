# Templates

There is a template pack for Aksio projects.
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

Once created, you can simply run the microservice by running the following from the `Main` folder:

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
