import { IQueryFor } from './IQueryFor';
import { QueryResult } from "./QueryResult";
import Handlebars from 'handlebars';

/**
 * Represents an implementation of {@link IQueryFor}.
 * @template TModel Type of model.
 */
export abstract class QueryFor<TModel, TArguments = {}> implements IQueryFor<TModel, TArguments> {
    abstract readonly route: string;
    abstract readonly routeTemplate: Handlebars.TemplateDelegate;

    /** @inheritdoc */
    async perform(args?: TArguments): Promise<QueryResult<TModel>> {
        let actualRoute = this.route;
        if (args && Object.keys(args).length > 0) {
            actualRoute = this.routeTemplate(args);
        }

        const response = await fetch(actualRoute, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return await QueryResult.fromResponse<TModel>(response);
    }
}
