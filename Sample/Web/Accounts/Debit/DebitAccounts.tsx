import { useMemo } from 'react';
import { useDialog, DialogResult } from '../../useDialog';
import { CreateAccountDialog, CreateAccountDialogResult } from './CreateAccountDialog';

import {
    CommandBar,
    IColumn,
    ICommandBarItemProps,
    DetailsList,
    Selection,
    SelectionMode,
    Stack
} from '@fluentui/react';

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


export const DebitAccounts = () => {

    const [showCreateAccount, createAccountDialogProps] = useDialog<any, CreateAccountDialogResult>(async (result, output?) => {
        if (result === DialogResult.Success && output) {
            const createDebitAccount: CreateDebitAccount = {
                accountId: '4cf10c68-2917-4c13-842e-73e69c9fda47',
                name: output.name,
                owner: 'edd60145-a6df-493f-b48d-35ffdaaefc4c'
            };

            fetch('/api/accounts/debit', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(createDebitAccount)
            });
        }
    });

    const commandBarItems: ICommandBarItemProps[] = [
        {
            key: 'add',
            name: 'Add Debit Account',
            iconProps: { iconName: 'Add' },
            onClick: () => {
                showCreateAccount();
            }
        },
    ];

    const items: any[] = [];

    const selection = useMemo(
        () => new Selection({
            selectionMode: SelectionMode.single,
            onSelectionChanged: () => {
                const selected = selection.getSelection();
                if (selected.length === 1) {
                    alert(selected[0]);
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
        </>
    );
};