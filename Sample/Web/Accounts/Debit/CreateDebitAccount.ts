/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { ICommand } from '@aksio/frontend/commands';
import { Guid } from '@cratis/fundamentals';

export class CreateDebitAccount implements ICommand {
    get route(): string {
        return '/api/accounts/debit';
    }

    accountId!: Guid;
    name!: string;
    owner!: Guid;
}
