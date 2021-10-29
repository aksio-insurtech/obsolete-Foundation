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

```typescript
export const MyComponent = () => {
    const [accounts, queryAccounts] = AllAccounts.use();

    return (
        <>
        </>
    )
};
```
