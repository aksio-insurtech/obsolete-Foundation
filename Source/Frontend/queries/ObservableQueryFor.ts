import { IObservableQueryFor, OnNextResult } from './IObservableQueryFor';
import Handlebars from 'handlebars';

/**
 * Represents an implementation of {@link IQueryFor}.
 * @template TDataType Type of data returned by the query.
 */
export abstract class ObservableQueryFor<TDataType, TArguments = {}> implements IObservableQueryFor<TDataType, TArguments> {
    abstract readonly route: string;
    abstract readonly routeTemplate: Handlebars.TemplateDelegate<any>;

    abstract readonly defaultValue: TDataType;
    abstract readonly requiresArguments: boolean;

    subscribe(callback: OnNextResult, args?: TArguments): void {
        let actualRoute = this.route;
        if (args && Object.keys(args).length > 0) {
            actualRoute = this.routeTemplate(args);
        }

        const secure = document.location.protocol.indexOf('https') === 0;
        const url = `${secure ? 'wss' : 'ws'}://${document.location.host}${actualRoute}`;
        const socket = new WebSocket(url);
        socket.onopen = (ev) => {
            console.log(`Connection for '${actualRoute}' established`);
        };
        socket.onerror = (error) => {
            console.log(`Error with connection for '${actualRoute} - ${error}`);
        };
        socket.onmessage = (ev) => {
            callback(JSON.parse(ev.data));
        };
    }
}