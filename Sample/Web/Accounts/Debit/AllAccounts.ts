/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { QueryFor, QueryResult, useQuery, PerformQuery } from '@aksio/frontend/queries';
import { DebitAccount } from './DebitAccount';

export class AllAccounts extends QueryFor<DebitAccount> {
    readonly route: string = '/api/accounts/debit';

    static use(): [QueryResult<DebitAccount>, PerformQuery] {
        return useQuery<DebitAccount, AllAccounts>(AllAccounts);
    }
}
