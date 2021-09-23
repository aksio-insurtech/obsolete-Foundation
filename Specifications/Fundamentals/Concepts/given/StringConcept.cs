namespace Aksio.Concepts.given
{
    public record StringConcept(string Value) : ConceptAs<string>(Value)
    {
        public static implicit operator StringConcept(string value) => new(value);
    }
}
