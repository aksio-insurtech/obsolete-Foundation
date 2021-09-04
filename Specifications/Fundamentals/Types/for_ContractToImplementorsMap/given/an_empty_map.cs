namespace Aksio.Types.for_ContractToImplementorsMap.given
{
    public class an_empty_map : Specification
    {
        protected ContractToImplementorsMap map;

        void Establish() => map = new();
    }
}
