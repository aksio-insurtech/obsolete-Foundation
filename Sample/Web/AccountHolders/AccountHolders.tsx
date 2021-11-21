import { useState } from 'react';
import { CommandBar, DetailsList, IColumn, ICommandBarItemProps, PrimaryButton, SearchBox, Stack } from '@fluentui/react';
import { AllAccountHolders } from './AllAccountHolders';
import { AccountHoldersStartingWith } from './AccountHoldersStartingWith';

const columns: IColumn[] = [
    {
        key: 'firstName',
        name: 'FirstName',
        fieldName: 'firstName',
        minWidth: 200
    },
    {
        key: 'lastName',
        name: 'LastName',
        fieldName: 'lastName',
        minWidth: 200
    }
];


export const AccountHolders = () => {
    const [accountHolders, queryAccountHolders] = AllAccountHolders.use();
    const [accountHoldersStartingWith, queryAccountsStartingWith] = AccountHoldersStartingWith.use({ filter: '' });
    const [searching, setSearching] = useState<boolean>(false);

    const searchFor = (filter: string) => {
        if (filter && filter !== '') {
            setSearching(true);
        } else {
            setSearching(false);
        }
        queryAccountsStartingWith({ filter });
    };

    const triggerIntegration = async () => {
        await fetch('/api/integration', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        setTimeout(() => queryAccountHolders(), 50);
    };

    const commandBarItems: ICommandBarItemProps[] = [
        {
            key: 'import',
            name: 'Trigger import',
            iconProps: { iconName: 'Add' },
            onClick: () => {
                triggerIntegration();
            }
        },
        {
            key: 'refresh',
            name: 'Refresh',
            iconProps: { iconName: 'Refresh' },
            onClick: () => queryAccountHolders()
        },
        {
            key: 'search',
            onRender: (props, defaultRenderer) => {
                return (
                    <div style={{ position: 'relative', top: '6px', width: '400px' }}>
                        <SearchBox
                            placeholder="AccountHolders starting with"
                            onClear={() => searchFor('')}
                            onChange={(ev, newValue) => searchFor(newValue || '')} />
                    </div>
                );
            }
        }
    ];

    const items = searching ? accountHoldersStartingWith.data : accountHolders.data;

    return (
        <div>
            <Stack>
                <Stack.Item disableShrink>
                    <CommandBar items={commandBarItems} />
                </Stack.Item>
                <Stack.Item>
                    <DetailsList columns={columns} items={items} />
                </Stack.Item>
            </Stack>
        </div>
    );
};