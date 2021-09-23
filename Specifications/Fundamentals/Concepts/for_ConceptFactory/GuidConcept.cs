namespace Aksio.Concepts.for_ConceptFactory
{
    public record GuidConcept(Guid Value) : ConceptAs<Guid>(Value);
}
