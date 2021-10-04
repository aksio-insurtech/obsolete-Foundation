# Templates

There is a template pack for Aksio projects.
Install it by simply doing the following from your terminal:

```shell
$ dotnet new -i Aksio.Templates
```

The templates are also supported through Visual Studio
by enabling [.NET CLI tooling](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/).

## Usage

```shell
$ dot new <template-name>
```

| Template | Description |
| -------- | ----------- |
| aksioms  | Aksio Microservice template with ASP.NET Core for .NET 6 |

### AKSIOMS - Aksio Microservice

The Aksio Microservice template comes with an optional Web. By adding the `--IncludeWeb` option to
the command line, you'll get a setup with a React frontend with WebPack ready to go.