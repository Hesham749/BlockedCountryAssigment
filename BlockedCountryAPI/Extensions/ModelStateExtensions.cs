using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlockedCountryAPI.Extensions;

public static class ModelStateExtensions
{
    public static bool TryValidateObjectAndAddErrors(this ModelStateDictionary modelState, object obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (!isValid)
        {
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    modelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }
        }

        return isValid;
    }
}