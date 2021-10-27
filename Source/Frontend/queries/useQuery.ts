export type RefreshQuery = () => Promise<void>;

export function useQuery<TQuery, TModel>(): [TModel[], RefreshQuery] {
    return [[], async () => {}];
}