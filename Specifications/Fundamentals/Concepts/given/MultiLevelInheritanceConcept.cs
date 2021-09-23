namespace Aksio.Concepts.given
{
    public record MultiLevelInheritanceConcept(long Value) : InheritingFromLongConcept(Value)
    {
        public static implicit operator MultiLevelInheritanceConcept(long value) => new(value);
    }
}
