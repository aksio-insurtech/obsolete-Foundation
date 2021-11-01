/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { QueryFor, QueryResult, useQuery, PerformQuery } from '@aksio/frontend/queries';
import { DebitAccount } from './DebitAccount';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/accounts/debit/some/{{category}}?startingWith={{startingWith}}');

export interface SomeAccountsArguments {
    category: string;
    startingWith?: string;
}
export class SomeAccounts extends QueryFor<DebitAccount, SomeAccountsArguments> {
    readonly route: string = '/api/accounts/debit/some/{{category}}?startingWith={{startingWith}}';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;

    static use(args: SomeAccountsArguments): [QueryResult<DebitAccount>, PerformQuery<SomeAccountsArguments>] {
        return useQuery<DebitAccount, SomeAccounts, SomeAccountsArguments>(SomeAccounts, args);
    }
}
