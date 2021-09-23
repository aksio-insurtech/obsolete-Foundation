namespace Aksio.Concepts.for_StringExtensions
{
    public record ConceptAsGuid(Guid Value) : ConceptAs<Guid>(Value)
    {
        public static implicit operator ConceptAsGuid(Guid id) => new (id);
    }
}
