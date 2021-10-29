/**
 * Represents the result from executing a {@link ICommand}.
 */
export class CommandResult {
    private _ok: boolean;

    /**
     * Creates an instance of command result.
     * @param {Response} response The HTTP response.
     */
    constructor(response: Response) {
        this._ok = response.ok;
    }

    /**
     * Gets whether execution is successful or not.
     */
    get isSuccess() {
        return this._ok;
    }
}
