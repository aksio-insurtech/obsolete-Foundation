using System.Globalization;

namespace Integration.AccountHolders
{
    public class ExternalAccountHolderSystem : IExternalAccountHolderSystem
    {
        public Task<ExternalAccountHolder> GetBySocialSecurityNumber(string socialSecurityNumber) =>
            Task.FromResult(new ExternalAccountHolder(
                "20107512345",
                "Einar",
                "Ingebrigtsen",
                new DateTime(1975, 10, 20),
                "20107512345",
                "Hansåsen 9",
                "Sandefjord",
                "3230",
                "Norway"));
        public Task<IEnumerable<ExternalAccountHolder>> GetBySocialSecurityNumbers(IEnumerable<string> socialSecurityNumbers) => throw new NotImplementedException();
    }
}