using Microsoft.AspNetCore.Components.Forms;

namespace GameFellowship.Data.FormModel;

public class BootstrapValidationClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

        if (editContext.IsModified(fieldIdentifier))
        {
            return isValid ? "modified valid" : "modified invalid";
        }

        return isValid ? "valid" : "invalid";
    }
}