# Contributing

Before you start contributing, you should read our guidelines [here](https://github.com/aksio-system/Home/blob/main/README.md).

## Prerequisites

This repository requires the following:

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://docs.docker.com/get-docker/)

## Build and Test

All C# based projects are added to the solution file at the root level, you can therefor
build it quite easily from root:

```shell
$ dotnet build
```

Similarly with the specifications you can do:

```shell
$ dotnet test
```

If you're using an IDE such as Visual Studio, Rider or similar - open the [Foundation.sln](./Foundation.sln)
file and do the build / test run from within the IDE.

## Static Code Analysis

All projects are built using the same static code analysis rules found [here](https://github.com/aksio-system/Defaults).
You can find the rule-sets [here](https://github.com/aksio-system/Defaults/tree/main/Source/Defaults).
