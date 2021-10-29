import { QueryResult } from './QueryResult';

/**
 * Defines the base of a query.
 * @template TModel Type of model the query is for.
 * @template TArguments Optional type of arguments to use for the query.
 */
export interface IQueryFor<TModel, TArguments = {}> {
    readonly route: string;

    /**
     * Perform the query.
     * @param [args] Optional arguments for the query - depends on whether or not the query needs arguments.
     * @returns {QueryResult} for the model
     */
    perform(args?: TArguments): Promise<QueryResult<TModel>>;
}