namespace Concepts
{
    public record PersonId(Guid Value) : ConceptAs<Guid>(Value);
}