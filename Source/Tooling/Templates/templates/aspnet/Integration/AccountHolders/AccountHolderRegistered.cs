using System.Collections.Generic;
using Aksio.Integration;
using Cratis.Changes;
using Cratis.Events.Projections;
using Cratis.Events.Projections;

namespace Integration.AccountHolders
{
    [EventType("48447c3e-f99e-449f-80c6-15425859ce61")]
    public record AccountHolderRegistered(string FirstName, string LastName, DateTime DateOfBirth);
}