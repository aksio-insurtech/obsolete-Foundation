using System.Collections.Generic;
using Aksio.Integration;
using Cratis.Changes;
using Cratis.Events.Projections;
using Cratis.Events.Projections;

namespace Integration.AccountHolders
{
    public record AccountHolderAddressChanged(string Address, string City, string PostalCode, string Country);
}