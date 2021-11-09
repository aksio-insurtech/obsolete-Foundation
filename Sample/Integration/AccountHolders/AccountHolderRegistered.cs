using System.Collections.Generic;
using Aksio.Integration;
using Cratis.Changes;
using Cratis.Events.Projections;
using Cratis.Events.Projections;

namespace Integration.AccountHolders
{
    public record AccountHolderRegistered(string FirstName, string LastName, DateTime DateOfBirth);
}