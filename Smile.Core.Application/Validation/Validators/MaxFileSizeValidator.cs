using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Helpers;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Validation.Validators
{
    public class MaxFileSizeValidator : ValidationAttribute
    {
        private readonly int maxFileSize;
        private readonly bool isCollection;

        public MaxFileSizeValidator(int maxFileSize, bool isCollection = false)
        {
            this.maxFileSize = maxFileSize;
            this.isCollection = isCollection;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!isCollection)
            {
                var file = value as IFormFile;

                if (file != null && file.Length > maxFileSize * Constants.UnitConversionMultiplier * Constants.UnitConversionMultiplier)
                    return new ValidationResult(ValidatorMessages.MaxFileSizeValidatorMessage(maxFileSize));
            }
            else
            {
                var files = value as List<IFormFile>;

                if (files != null)
                    foreach (var file in files)
                        if (file.Length > maxFileSize * Constants.UnitConversionMultiplier * Constants.UnitConversionMultiplier)
                            return new ValidationResult(ValidatorMessages.MaxFileSizeValidatorMessage(maxFileSize));
            }

            return ValidationResult.Success;
        }
    }
}