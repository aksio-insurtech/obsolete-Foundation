namespace Aksio.Concepts.given
{
    public record LongConcept(long Value) : ConceptAs<long>(Value)
    {
        public static implicit operator LongConcept(long value) => new(value);
    }
}
