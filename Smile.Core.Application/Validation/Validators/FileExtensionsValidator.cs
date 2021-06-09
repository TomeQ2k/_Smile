using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Helpers;

namespace Smile.Core.Application.Validation.Validators
{
    public class FileExtensionsValidator : ValidationAttribute
    {
        private readonly string[] extensions;

        public bool IsCollection { get; set; }

        public FileExtensionsValidator(params string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!IsCollection)
            {
                if (!IsValidExtension(value as IFormFile))
                    return new ValidationResult(ValidatorMessages.FileExtensionsValidatorMessage(extensions));
            }
            else
            {
                var files = value as List<IFormFile>;

                foreach (var file in files)
                    if (!IsValidExtension(file))
                        return new ValidationResult(ValidatorMessages.FileExtensionsValidatorMessage(extensions));
            }

            return ValidationResult.Success;
        }

        #region private

        private bool IsValidExtension(IFormFile file) => extensions.Any(e => e == Path.GetExtension(file.FileName.ToLower()));

        #endregion
    }
}