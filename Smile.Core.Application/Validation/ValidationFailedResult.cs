using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smile.Core.Application.Features.Responses;

namespace Smile.Core.Application.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState, Models.Error.Error error)
            : base(new ValidationResponse(modelState, error))
                => (StatusCode) = (StatusCodes.Status422UnprocessableEntity);
    }
}