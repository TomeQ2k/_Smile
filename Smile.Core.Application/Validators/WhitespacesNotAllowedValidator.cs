using System.ComponentModel.DataAnnotations;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Helpers;

namespace Smile.Core.Application.Validators
{
    public class WhitespacesNotAllowedValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = (string)value;

            if (val.HasWhitespaces())
                return new ValidationResult(ValidatorMessages.WhitespacesNotAllowedValidatorMessage);

            return ValidationResult.Success;
        }
    }
}