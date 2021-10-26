/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { Guid } from '@cratis/fundamentals';

export class DepositToAccount extends ICommand {
    get route(): string {
        return '/api/accounts/debit/deposit';
    }

    accountId!: Guid;
    amount!: number;
}