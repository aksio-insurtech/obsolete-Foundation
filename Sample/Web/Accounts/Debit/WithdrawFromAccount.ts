/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { ICommand } from '@aksio/frontend/commands';
import { Guid } from '@cratis/fundamentals';

export class WithdrawFromAccount implements ICommand {
    get route(): string {
        return '/api/accounts/debit/withdraw';
    }

    accountId!: Guid;
    amount!: number;
}
