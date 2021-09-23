namespace Aksio.Concepts.given
{
    public record IntConcept(int Value) : ConceptAs<int>(Value)
    {
        public static implicit operator IntConcept(int value) => new(value);
    }
}
