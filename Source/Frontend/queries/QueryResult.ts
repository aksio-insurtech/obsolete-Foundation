
type QueryResultFromServer<TModel> = {
    items: TModel[];
    isSuccess: boolean;
};

/**
 * Represents the result from executing a {@link IQueryFor}.
 * @template TModel The model type.
 */
export class QueryResult<TModel> {
    /**
     * Creates an instance of query result.
     * @param {TModel[]} items The items returned, if any - can be empty.
     * @param {boolean} isSuccess Whether or not the query was successful.
     */
    constructor(readonly items: TModel[], readonly isSuccess: boolean) {
    }

    /**
     * Create a {@link QueryResult} from a {@link Response}.
     * @template TModel Type of model to create for.
     * @param {Response} [response] Response to create from.
     * @returns A new {@link QueryResult}.
     */
    static async fromResponse<TModel>(response: Response): Promise<QueryResult<TModel>> {
        const jsonResponse = await response.json() as QueryResultFromServer<TModel>;
        return new QueryResult(jsonResponse.items, jsonResponse.isSuccess && response.ok);
    }
}
