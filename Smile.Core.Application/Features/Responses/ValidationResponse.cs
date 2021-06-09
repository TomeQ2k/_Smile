using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smile.Core.Application.Models.Error;
using Smile.Core.Application.Validation;

namespace Smile.Core.Application.Features.Responses
{
    public class ValidationResponse : BaseResponse
    {
        public IDictionary<string, ValidationError> ValidationErrors { get; }

        public ValidationResponse(ModelStateDictionary modelState, Error error = null)
            : base(error)
            => (ValidationErrors) = (modelState.Keys
                .GroupBy(key => key, key => modelState[key].Errors.Select(e => e.ErrorMessage))
                .ToDictionary(g => g.Key, g => new ValidationError(g.Key, g.First().ToList())));
    }
}