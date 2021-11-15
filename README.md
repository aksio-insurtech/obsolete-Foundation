# Introduction

[![Build](https://github.com/aksio-system/Foundation/actions/workflows/build.yml/badge.svg)](https://github.com/aksio-system/Foundation/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/aksio.microservices)](http://nuget.org/packages/aksio.microservices)

This repository holds the generalized abstractions being the foundation of solutions built @ Aksio.
Within this you'll find reusable components, libraries, helpers and solutions that are
cross cutting. Its goal is to provide developers with a productivity suite.

Read the [documentation](./Documentation/index.md) for all the details.
For general guidance on the core values and principles we @ Aksio use, read more [here](https://github.com/aksio-system/Home/blob/main/README.md).

To get a quick start, head over to our [getting started](./Documentation/getting-started.md) section.

## Running the sample

Make sure you have the following installed:

- [Docker](https://www.docker.com/products/docker-desktop)
- [.NET Core 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node JS version 16](https://nodejs.org/)

The sample consists of a backend and a frontend.
Navigate to the [Sample](./Sample) folder.

Before running the microservice backend and frontend, we will need to run the Dolittle Runtime

```shell
docker run -d -p 50052:50052 -p 50053:50053 -p 27017:27017 dolittle/runtime:latest-development
```

> Note: If you're running with an ARM64 based computer, such as the Apple M based Macs, you'll need
> a different image; dolittle/runtime:latest-arm64-development.
> `docker run -d -p 50052:50052 -p 50053:50053 -p 27017:27017 dolittle/runtime:latest-arm64-development`

Within here you'll see a folder called [Main](./Sample/Main), which represents the backend.
Navigate to this and start the backend by running:

```shell
$ dotnet run
```

The frontend is located in the [Web](./Sample/Web) folder. While the backend is running in another terminal,
navigate to that folder and start it by running:

```shell
$ yarn start:dev
```

Open a browser and navigate to [http://localhost:9000/](http://localhost:9000/) and you can start playing
around with the sample.
