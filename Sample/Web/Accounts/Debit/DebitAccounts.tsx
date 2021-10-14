import { useMemo, useState } from 'react';
import { useDialog, DialogResult } from '../../useDialog';
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

type CreateDebitAccount = {
    accountId: string;
    name: string,
    owner: string
};

type DepositToAccount = {
    accountId: string;
    amount: number;
};

export const DebitAccounts = () => {
    const [items, refreshItems] = useDataFrom('/api/accounts/debit');
    const [selectedItem, setSelectedItem] = useState<any>(undefined);
    const [showCreateAccount, createAccountDialogProps] = useDialog<any, CreateAccountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output) {
            const createDebitAccount: CreateDebitAccount = {
                accountId: Guid.create().toString(),
                name: output.name,
                owner: 'edd60145-a6df-493f-b48d-35ffdaaefc4c'
            };

            await fetch('/api/accounts/debit', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(createDebitAccount)
            });

            setTimeout(refreshItems, 200);
        }
    });


    const [showAmountDialog, amountDialogProps] = useDialog<AmountDialogInput, AmountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output && selectedItem) {
            const depositToAccount: DepositToAccount = {
                accountId: selectedItem.id,
                amount: output.amount
            };

            await fetch('/api/accounts/debit/deposit', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(depositToAccount)
            });

            setTimeout(refreshItems, 200);
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
                onClick: () => showAmountDialog({ okTitle: 'Deposit' })
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
            <AmountDialog {...amountDialogProps} />
        </>
    );
};