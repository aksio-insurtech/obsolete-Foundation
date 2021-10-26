/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { Guid } from '@cratis/fundamentals';

export class WithdrawFromAccount extends ICommand {
    get route(): string {
        return '/api/accounts/debit/withdraw';
    }

    accountId!: Guid;
    amount!: number;
}