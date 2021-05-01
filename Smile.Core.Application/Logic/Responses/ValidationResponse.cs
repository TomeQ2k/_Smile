using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smile.Core.Application.Models.Error;
using Smile.Core.Application.Models.Validation;

namespace Smile.Core.Application.Logic.Responses
{
    public class ValidationResponse : BaseResponse
    {
        public List<ValidationError> ValidationErrors { get; }

        public ValidationResponse(ModelStateDictionary modelState, Error error = null)
            : base(error)
            => (ValidationErrors) = (modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(e => new ValidationError(key, e.ErrorMessage)))
                .ToList());
    }
}