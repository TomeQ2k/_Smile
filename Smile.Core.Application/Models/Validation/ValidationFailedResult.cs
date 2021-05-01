using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smile.Core.Application.Logic.Responses;

namespace Smile.Core.Application.Models.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState, Error.Error error)
            : base(new ValidationResponse(modelState, error))
                => (StatusCode) = (StatusCodes.Status422UnprocessableEntity);
    }
}