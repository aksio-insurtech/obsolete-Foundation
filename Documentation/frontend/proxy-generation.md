# Proxy Generation

To bridge the gap between the frontend and the backend, there is a tool for generating what we call proxies.
These are representations to be used in the frontend for artifacts in the backend. These are primarily grouped into 2
types; Commands & Queries.

## Commands

Commands are the things you want to perform. These are represented as **HttpPost** operations on controllers and take a
complex type as input through the body of the Http request. The generator will generate commands based on the type and
include the route information into the generated object.

## Queries

Queries are the data coming out. These are represents as **HttpGet** operations on controllers and returns an enumerable
of a specific type. These can have arguments which will also be part of the proxy objects. The generator will use the
method name as the query name, so remember to name these properly to get meaningful query objects for the frontend.

You can provide parameters to the queries as well. These can either be part of the route or as part of the query string.
(C#: `[FromRoute]` or `[FromQuery]`). The proxy generator will create a type that holds these and becomes compile-time
checked when using the query in the frontend.

## Getting started

All you need is to reference the following **Aksio.ProxyGenerator** package and configure the property for the output
folder within your **.csproj** file. Lets say you have a structure as below:

```shell
<Your Root Folder>
|
+-- Domain
+-- Read
+-- Web
```

The **Domain** and **Read** projects could typically then have ASP.NET Controllers within them representing commands and queries.
The **Web** project being where you have your web code and the place you want the files to be generated. In the **.csproj**
files for the **Domain** and **Read** you would then add a dependency to the NuGet package and add the following property:

```xml
<PropertyGroup>
    <AksioProxyOutput>$(MSBuildThisFileDirectory)../Web</AksioProxyOutput>
</PropertyGroup>
```

The generator will maintain the folder structure from the source files while generating based on the namespaces of the files.
