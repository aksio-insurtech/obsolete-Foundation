using Cratis.Changes;
using ObjectsComparer;

namespace Aksio.Integration.for_ImportBuilderExtensions.given
{
    public class a_change_on_one_property : no_changes
    {
        void Establish()
        {
            modified_model = new Model(42, "Forty Three");
            original_model = new Model(42, "Forty Two");

            changeset.Add(new PropertiesChanged<Model>(modified_model, new[]
            {
                new PropertyDifference<Model>(
                    original_model,
                    modified_model,
                    new Difference(
                        nameof(Model.SomeString),
                        original_model.SomeString,
                        modified_model.SomeString,
                        DifferenceTypes.ValueMismatch))
            }));
        }
    }
}