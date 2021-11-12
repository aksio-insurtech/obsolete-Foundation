using System.Globalization;

namespace Integration.AccountHolders
{
    public class ExternalAccountHolderSystem : IExternalAccountHolderSystem
    {
        public Task<ExternalAccountHolder> GetBySocialSecurityNumber(string socialSecurityNumber) =>
            Task.FromResult(new ExternalAccountHolder(
                "03050712345",
                "John",
                "Doe",
                new DateTime(2007, 5, 3),
                "03050712345",
                "Greengrass 42",
                "Paradise City",
                "48321",
                "Themyscira"));
        public Task<IEnumerable<ExternalAccountHolder>> GetBySocialSecurityNumbers(IEnumerable<string> socialSecurityNumbers) => throw new NotImplementedException();
    }
}