import { useMemo, useState } from 'react';
import { useDialog, DialogResult } from '@aksio/frontend/dialogs';
import { CreateAccountDialog, CreateAccountDialogResult } from './CreateAccountDialog';
import { useDataFrom } from '../../useDataFrom';
import { Guid } from '@cratis/fundamentals';

import {
    CommandBar,
    IColumn,
    ICommandBarItemProps,
    DetailsList,
    Selection,
    SelectionMode,
    Stack
} from '@fluentui/react';
import { AmountDialog, AmountDialogInput, AmountDialogResult } from './AmountDialog';
import { CreateDebitAccount } from './CreateDebitAccount';
import { DepositToAccount } from './DepositToAccount';
import { WithdrawFromAccount } from './WithdrawFromAccount';

const columns: IColumn[] = [
    {
        key: 'name',
        name: 'Name',
        fieldName: 'name',
        minWidth: 200
    },
    {
        key: 'balance',
        name: 'Balance',
        fieldName: 'balance',
        minWidth: 200
    }
];

export const DebitAccounts = () => {
    const [items, refreshItems] = useDataFrom('/api/accounts/debit');
    const [selectedItem, setSelectedItem] = useState<any>(undefined);
    const [showCreateAccount, createAccountDialogProps] = useDialog<any, CreateAccountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output) {
            const command = new CreateDebitAccount();
            command.accountId = Guid.create().toString(),
            command.name = output.name;
            command.owner = 'edd60145-a6df-493f-b48d-35ffdaaefc4c';
            await command.execute();
            setTimeout(refreshItems, 20);
        }
    });


    const [showDepositAmountDialog, depositAmountDialogProps] = useDialog<AmountDialogInput, AmountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output && selectedItem) {
            const command = new DepositToAccount();
            command.accountId = selectedItem.id;
            command.amount = output.amount;
            await command.execute();
            setTimeout(refreshItems, 20);
        }
    });

    const [showWithdrawAmountDialog, withdrawAmountDialogProps] = useDialog<AmountDialogInput, AmountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output && selectedItem) {
            const command = new WithdrawFromAccount();
            command.accountId = selectedItem.id;
            command.amount = output.amount;
            await command.execute();
            setTimeout(refreshItems, 20);
        }
    });


    const commandBarItems: ICommandBarItemProps[] = [
        {
            key: 'add',
            name: 'Add Debit Account',
            iconProps: { iconName: 'Add' },
            onClick: showCreateAccount
        },
        {
            key: 'refresh',
            name: 'Refresh',
            iconProps: { iconName: 'Refresh' },
            onClick: refreshItems
        }
    ];

    if (selectedItem) {
        commandBarItems.push(
            {
                key: 'deposit',
                name: 'Deposit',
                iconProps: { iconName: 'Money' },
                onClick: () => showDepositAmountDialog({ okTitle: 'Deposit' })
            }
        );

        commandBarItems.push(
            {
                key: 'withdraw',
                name: 'Withdraw',
                iconProps: { iconName: 'Money' },
                onClick: () => showWithdrawAmountDialog({ okTitle: 'Withdraw' })
            }
        );

    }

    const selection = useMemo(
        () => new Selection({
            selectionMode: SelectionMode.single,
            onSelectionChanged: () => {
                const selected = selection.getSelection();
                if (selected.length === 1) {
                    setSelectedItem(selected[0]);
                }
            },
            items: items
        }), [items]);


    return (
        <>
            <Stack>
                <Stack.Item disableShrink>
                    <CommandBar items={commandBarItems} />
                </Stack.Item>
                <Stack.Item>
                    <DetailsList columns={columns} items={items} selection={selection} />
                </Stack.Item>
            </Stack>

            <CreateAccountDialog {...createAccountDialogProps} />
            <AmountDialog {...depositAmountDialogProps} />
            <AmountDialog {...withdrawAmountDialogProps} />
        </>
    );
};