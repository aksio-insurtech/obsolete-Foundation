import { IQueryFor } from './IQueryFor';
import { QueryResult } from "./QueryResult";

/**
 * Represents an implementation of {@link IQueryFor}.
 * @template TModel Type of model.
 */
export abstract class QueryFor<TModel, TArguments = {}> implements IQueryFor<TModel, TArguments> {
    abstract readonly route: string;

    /** @inheritdoc */
    async perform(args?: TArguments): Promise<QueryResult<TModel>> {
        const response = await fetch(this.route, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        const items = await response.json();
        return new QueryResult(items, response);
    }
}
