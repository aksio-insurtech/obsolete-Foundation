using Events.AccountHolders;

namespace Read.AccountHolders
{
    public class AccountHolderProjection : IProjectionFor<AccountHolder>
    {
        public void Define(IProjectionBuilderFor<AccountHolder> builder) => builder
            .From<AccountHolderRegistered>(_ => _
                .Set(m => m.FirstName).To(ev => ev.FirstName)
                .Set(m => m.LastName).To(ev => ev.LastName)
                .Set(m => m.DateOfBirth).To(ev => ev.DateOfBirth))
            .From<AccountHolderAddressChanged>(_ => _
                .Set(m => m.Address).To(ev => ev.Address)
                .Set(m => m.City).To(ev => ev.City)
                .Set(m => m.PostalCode).To(ev => ev.PostalCode)
                .Set(m => m.Country).To(ev => ev.Country));
    }
}