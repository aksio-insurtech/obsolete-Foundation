using System.Collections.Generic;
using Aksio.Integration;
using Cratis.Changes;
using Cratis.Events.Projections;
using Cratis.Events.Projections;

namespace Integration.AccountHolders
{
    [EventType("ccb6afd1-e47e-45e0-8d37-6d2a8d071344")]
    public record AccountHolderAddressChanged(string Address, string City, string PostalCode, string Country);
}