namespace Aksio.Concepts.for_StringExtensions
{
    public record ConceptAsLong(long Value) : ConceptAs<long>(Value)
    {
        public static implicit operator ConceptAsLong(long value) => new (value);
    }
}
