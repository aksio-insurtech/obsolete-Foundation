/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { Guid } from '@cratis/fundamentals';

export class CreateDebitAccount extends ICommand {
    get route(): string {
        return '/api/accounts/debit';
    }

    accountId!: Guid;
    name!: string;
    owner!: Guid;
}