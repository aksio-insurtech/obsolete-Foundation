namespace Concepts.Accounts
{
    public record AccountName(string Value) : ConceptAs<string>(Value);
}