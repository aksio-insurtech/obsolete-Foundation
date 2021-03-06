import Handlebars from 'handlebars';
import { ObservableQuerySubscription } from './ObservableQuerySubscription';

/**
 * The delegate type representing the callback of result from the server.
 */
export type OnNextResult = <TDataType>(data: TDataType) => void;

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
     * Subscribe to the query. This will create a subscription onto the server.
     * @param {OnNextResult}┬ácallback The callback that will receive result from the server.
     * @param [args] Optional arguments for the query - depends on whether or not the query needs arguments.
     * @returns {ObservableQuerySubscription<TDataType>}.
     */
    subscribe(callback: OnNextResult, args?: TArguments): ObservableQuerySubscription<TDataType>;
}
