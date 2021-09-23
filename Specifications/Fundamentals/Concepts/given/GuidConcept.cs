namespace Aksio.Concepts.given
{
    public record GuidConcept(Guid Value) : ConceptAs<Guid>(Value)
    {
        public static implicit operator GuidConcept(Guid value) => new(value);
    }
}
