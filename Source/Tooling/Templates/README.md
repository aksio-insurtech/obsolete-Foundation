# Templates

This project is a template project for .NET CLI tooling, it provides a [template pack](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-template-pack)
for productivity purposes. The templates can be leveraged using the `dotnet new` command and is also supported through Visual Studio
by enabling [.NET CLI tooling](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/).

For authoring create templates and whats possible, you can read more [here](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template).
Go [here](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates) for the full article on custom templates.

You can also find more details in the [DotNet Templating Wiki](https://github.com/dotnet/templating/wiki).

## Template Parameters & Symbols

Its possible to have parameters for templates, these are defined as symbols in the template config.
You can read about how they are [here](https://github.com/dotnet/templating/wiki/Reference-for-template.json#symbols).
If you want to have generated symbols, there are a few [prebuilt ones](https://github.com/dotnet/templating/wiki/Available-Symbols-Generators).

With symbols it is also possible to do [conditional processing](https://github.com/dotnet/templating/wiki/Conditional-processing-and-comment-syntax).

## Testing

To test the template pack, simply install it by doing the following from a terminal:

```shell
$ dotnet new -i ./
```

Then you can go ahead and run `dotnet new <short name of template>`.
To see if the templates are installed you can do `dotnet new --list` and you should see all the
Aksio templates at the top of the list.

To uninstall it when you're done testing:

```shell
$ dotnet new -u ./
```
