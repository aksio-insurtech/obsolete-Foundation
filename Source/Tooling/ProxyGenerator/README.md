# ProxyGenerator

This project is responsible for generating TypeScript code from backend C# code, specifically for ASP.NET controllers and
any input and/or output types they use on their actions.

It leverages the Roslyn Source Generator capability, originally intended for generating C# code - but fits our needs as
to get called at the correct time during the compilation steps and then output TypeScript code to the configured locations.

More information about Source Generators can be found [here](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)
and also a full tutorial can be found [here](https://www.thinktecture.com/en/net/roslyn-source-generators-introduction/).
The [following article](https://dominikjeske.github.io/source-generators/) contains a lot more detail as well.

https://www.mytechramblings.com/posts/configure-roslyn-analyzers-using-editorconfig/


## Run in sample

```shell
$ dotnet build --no-incremental
```

## Debugging

Debugging the compiler is not something that is typically not an out of the box experience.
If you're using regular Visual Studio 20xx, you can simply add a `Debugger.Launch()` statement in the code
where you want to debug and then run the build.

For VSCode, you have to wait for the debugger to attach to the correct process, add the following:

```csharp
while (!System.Diagnostics.Debugger.IsAttached) Thread.Sleep(10);
```

Then do a build from the project you're testing from:

```shell
$ dotnet build --no-incremental
```

You can then attach the debugger:

![](./attach-debugger.png)

... and find the compiler process:

![](./find-process.png)

You will then hit any breakpoints in the generator code.
