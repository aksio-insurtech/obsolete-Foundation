# Third Parties

The Aksio Foundation is built on top of existing 3rd parties in addition to the .NET Core and ASP.NET Core frameworks.

## Dolittle

All Microservices leverages the event store and microservice concepts from [Dolittle](https://dolittle.io).
Recommend getting familiar with it from their [docs](https://dolittle.io/docs/).

Aksio also runs the microservices with Dolittle in their managed platform, you can read more
about the requirements and details about their platform [here](https://dolittle.io/docs/platform/requirements/).

> Note: The Aksio Foundation has encapsulated most of the concepts used and is also leveraging other 3rd parties
> that has implementations on top of Dolittle.

## Cratis

[Cratis](https://github.com/cratis/cratis) holds a full event sourcing stack and has extensions for Dolittle on top. It contains concepts that are
not in the Dolittle runtime and/or SDK and also tooling that sits on top to make event sourcing second nature
for developers. In addition to this Cratis has a set of fundamental packages to make development easier.

You can read more about it in the project [docs](https://github.com/Cratis/cratis/tree/main/Documentation).

## MongoDB

At the core for storage, Aksio uses MongoDB and the Foundation together with Cratis provides abstractions to make it
easier to work with in conjunction with things like [tenancy](./tenancy.md).
