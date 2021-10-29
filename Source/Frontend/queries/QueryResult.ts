/**
 * Represents the result from executing a {@link IQueryFor}.
 * @template TModel The model type.
 */
export class QueryResult<TModel> {
    private readonly _ok: boolean;

    /**
     * Creates an instance of query result.
     * @param response 
     */
    constructor(readonly items: TModel[], response?: Response) {
        this._ok = response?.ok || true;
    }

    /**
     * Gets whether execution is successful or not.
     */
    get isSuccess() {
        return this._ok;
    }
}
