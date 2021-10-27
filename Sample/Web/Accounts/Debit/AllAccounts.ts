/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { IQueryFor, useQuery, RefreshQuery } from '@aksio/frontend/queries';
import { DebitAccount } from './DebitAccount';

export class AllAccounts implements IQueryFor<DebitAccount> {
    get route(): string {
        return '/api/accounts/debit';
    }

    static use(): [DebitAccount[], RefreshQuery] {
        return useQuery<AllAccounts, DebitAccount>();
    }
}
