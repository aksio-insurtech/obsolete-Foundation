namespace Aksio.Concepts.given
{
    public record InheritingFromLongConcept(long Value) : LongConcept(Value)
    {
        public static implicit operator InheritingFromLongConcept(long value) => new(value);
    }
}
