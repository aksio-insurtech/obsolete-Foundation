/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

import { QueryFor, QueryResult, useQuery, PerformQuery } from '@aksio/frontend/queries';
import { AccountHolder } from './AccountHolder';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/accountholders/starting-with?filter={{filter}}');

export interface StartingWithArguments {
    filter?: string;
}
export class StartingWith extends QueryFor<AccountHolder, StartingWithArguments> {
    readonly route: string = '/api/accountholders/starting-with?filter={{filter}}';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;

    static use(args: StartingWithArguments): [QueryResult<AccountHolder>, PerformQuery<StartingWithArguments>] {
        return useQuery<AccountHolder, StartingWith, StartingWithArguments>(StartingWith, args);
    }
}
