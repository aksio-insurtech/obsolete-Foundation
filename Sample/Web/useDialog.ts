import { useState } from 'react';

export interface IDialogProps<TInput = {}, TOutput = {}> {
    visible: boolean;
    input: TInput;
    onClose: DialogClosed<TOutput>
}

export enum DialogResult {
    Success,
    Failed,
    Cancelled
}

export type ShowDialog<T = {}> = (input?: T) => void;
export type DialogClosed<T> = (result: DialogResult, output?: T) => void;

export function useDialog<TInput = {}, TOutput = {}>(onClose: DialogClosed<TOutput>): [ShowDialog<TInput>, IDialogProps<TInput, TOutput>] {
    const [visible, setVisible] = useState(false);
    const [input, setInput] = useState({});

    const props = {
        visible,
        input,
        onClose: (result, output) => {
            setVisible(false);
            onClose(result, output);
        }
    } as IDialogProps<TInput, TOutput>;

    const show = (input?: TInput) => {
        if (input) {
            setInput(input);
        }
        setVisible(true);
    };

    return [
        show,
        props
    ];
}
