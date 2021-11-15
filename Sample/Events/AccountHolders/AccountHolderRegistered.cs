namespace Events.AccountHolders
{
    [EventType("48447c3e-f99e-449f-80c6-15425859ce61")]
    public record AccountHolderRegistered(string FirstName, string LastName, DateTime DateOfBirth);
}