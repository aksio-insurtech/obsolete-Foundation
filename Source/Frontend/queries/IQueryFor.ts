/**
 * Defines the base of a query.
 */
export interface IQueryFor {
    readonly route: string;
}

export type RefreshQuery = () => Promise<void>;

export function useQuery<TQuery, TModel>() {

}