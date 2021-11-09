namespace Integration.AccountHolders
{
    public interface IExternalAccountHolderSystem
    {
        Task<ExternalAccountHolder> GetBySocialSecurityNumber(string socialSecurityNumber);
        Task<IEnumerable<ExternalAccountHolder>> GetBySocialSecurityNumbers(IEnumerable<string> socialSecurityNumbers);
    }
}