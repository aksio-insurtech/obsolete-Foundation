# Queries

Queries represents data that is queryable in the system
There are encapsulated as objects that is the output of a HTTP Get controller action in the backend.
A query can have inputs in the form of arguments that is typically part of the routing information or as the querystring,
these can have validation rules around them.
In addition to this, the controller can have authorization policies associated with it that applies to the command.

## Proxy Generation

With the [proxy generator](./proxy-generation.md) you'll get the queries generated directly to use in the frontend.
This means you don't have to look at the Swagger API even to know what you have available, the code sits there directly
in the form of a generated proxy object. The generator will look at all HTTP Post actions during compile time and
look for actions marked with `[HttpGet]` and the return type being an enumerable of any object argument and assume that
this is your query representation / payload.

Take the following controller action in C#:

```csharp
[HttpGet]
public IEnumerable<DebitAccount> AllAccounts() => _collection.Find(_ => true).ToList();
```

And the read model in this case looking like:

```csharp
public record DebitAccount(AccountId Id, AccountName Name, PersonId Owner, double Balance);
```

This all gets generated into the following TypeScript code:

```typescript
import { QueryFor, QueryResult, useQuery, PerformQuery } from '@aksio/frontend/queries';
import { DebitAccount } from './DebitAccount';
import Handlebars from 'handlebars';


const routeTemplate = Handlebars.compile('/api/accounts/debit');

export class AllAccounts extends QueryFor<DebitAccount> {
    readonly route: string = '/api/accounts/debit';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;

    static use(): [QueryResult<DebitAccount>, PerformQuery] {
        return useQuery<DebitAccount, AllAccounts>(AllAccounts);
    }
}
```

## Usage

From a React component you can now use the static `use()` method:

```tsx
export const MyComponent = () => {
    const [accounts, queryAccounts] = AllAccounts.use();

    return (
        <>
        </>
    )
};
```

### Parameters

Queries can have parameters they can be used for instance for filtering.
Lets say you have a query called `StartingWith`:

```csharp
[HttpGet("starting-with")]
public IEnumerable<DebitAccount> StartingWith([FromQuery] string? filter)
{
    var filterDocument = Builders<DebitAccount>
        .Filter
        .Regex("name", $"^{filter ?? string.Empty}.*");

    return _collection.Find(filterDocument).ToList();
}
```

The `filter` parameter will be part of the generated proxy, since it has the `[FromQuery]`
attribute on it. Using the proxy requires you to now specify the parameter as well:

```tsx
export const MyComponent = () => {
    const [accounts, queryAccounts] = StartingWith.use({ filter: '' });

    return (
        <>
        </>
    )
};
```

> Note: Route values will also be considered parameters and generated when adorning
> a method parameter with `[HttpPost]`.
