import { CommandResult } from './CommandResult';

/**
 * Defines the base of a command.
 */
export interface ICommand {
    /**
     * Gets the route information for the command.
     */
    readonly route: string;

    /**
     * Execute the {@link ICommand}.
     */
    execute(): Promise<CommandResult>;
}
