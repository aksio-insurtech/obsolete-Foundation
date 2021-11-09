namespace Integration.AccountHolders
{
    public record ExternalAccountHolder(string Id, string FirstName, string LastName, DateTime DateOfBirth, string SocialSecurityNumber, string Address, string City, string PostalCode, string Country);
}