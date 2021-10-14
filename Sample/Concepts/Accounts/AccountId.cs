namespace Concepts.Accounts
{
    public record AccountId(Guid Value) : ConceptAs<Guid>(Value)
    {
        public static implicit operator EventSourceId(AccountId value) => new() { Value = value };
    }
}