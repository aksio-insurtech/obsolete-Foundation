import { QueryResult } from './QueryResult';
import { IQueryFor } from './IQueryFor';
import { Constructor } from '@cratis/fundamentals';
import { useState, useEffect } from 'react';

/**
 * Delegate type for performing a {@link IQueryFor} in the context of the {@link useQuery} hook.
 */
export type PerformQuery<TArguments = {}> = (args?: TArguments) => Promise<void>;

/**
 * React hook for working with {@link IQueryFor} within the state management of React.
 * @template TDataType Type of model the query is for.
 * @template TQuery Type of query to use.
 * @template TArguments Optional: Arguments for the query, if any 
 * @param query Query type constructor.
 * @returns Tuple of {@link QueryResult} and a {@link PerformQuery} delegate.
 */
export function useQuery<TDataType, TQuery extends IQueryFor<TDataType>, TArguments = {}>(query: Constructor<TQuery>, args?: TArguments): [QueryResult<TDataType>, PerformQuery<TArguments>] {
    const queryInstance = new query() as TQuery;
    const [result, setResult] = useState<QueryResult<TDataType>>(new QueryResult(queryInstance.defaultValue, true));
    const queryExecutor = (async (args?: TArguments) => {
        if (queryInstance.requiresArguments && !args) {
            console.log(`Warning: Query '${query.name}' requires arguments. Will not perform the query.`);
            return;
        }
        const response = await queryInstance.perform(args);
        setResult(response);
    });

    useEffect(() => {
        queryExecutor(args);
    }, []);

    return [result, queryExecutor];
}
