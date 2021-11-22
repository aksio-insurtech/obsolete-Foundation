import { QueryResult } from './QueryResult';
import Handlebars from 'handlebars';

/**
 * The delegate type representing the callback of result from the server.
 */
export type OnNextResult = <TDataType>(data: QueryResult<TDataType>) => void;

/**
 * Defines the base of a query.
 * @template TDataType Type of model the query is for.
 * @template TArguments Optional type of arguments to use for the query.
 */
export interface IObservableQueryFor<TDataType, TArguments = {}> {
    readonly route: string;
    readonly routeTemplate: Handlebars.TemplateDelegate;

    readonly defaultValue: TDataType;
    readonly requiresArguments: boolean;

    /**
     * Perform the query.
     * @param {OnNextResult} callback The callback that will receive result from the server.
     * @param [args] Optional arguments for the query - depends on whether or not the query needs arguments.
     * @returns {QueryResult} for the model
     */
    onNext(callback: OnNextResult, args?: TArguments): void;
}
