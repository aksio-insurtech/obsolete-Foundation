import { useState } from 'react';

import {
    Dialog,
    DialogFooter,
    DialogType,
    DefaultButton,
    IDialogContentProps,
    PrimaryButton,
    TextField,
} from '@fluentui/react';

import { IDialogProps, DialogResult } from '../../useDialog';

const dialogContentProps: IDialogContentProps = {
    type: DialogType.normal,
    title: 'Create Debit Account',
    closeButtonAriaLabel: 'Close'
};

export type CreateAccountDialogResult = {
    name: string;
};


export const CreateAccountDialog = (props: IDialogProps<any, CreateAccountDialogResult>) => {
    const [name, setName] = useState('');

    const create = () => {
        props.onClose(DialogResult.Success, { name });
    };

    const cancel = () => {
        props.onClose(DialogResult.Cancelled);
    };

    return (
        <Dialog
            minWidth={600}
            hidden={!props.visible}     // We react on the visible property from the props
            onDismiss={create}
            dialogContentProps={dialogContentProps}>

            <TextField label="Name" value={name} onChange={(ev, value) => setName(value!)} />

            <DialogFooter>
                <PrimaryButton onClick={create} text="Create" />
                <DefaultButton onClick={cancel} text="Cancel" />
            </DialogFooter>
        </Dialog>
    );
}