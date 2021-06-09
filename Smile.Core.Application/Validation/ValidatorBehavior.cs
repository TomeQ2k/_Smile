using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Models.Error;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Validation
{
    public class ValidatorBehavior : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState, Error.Build(ErrorCodes.ValidationError, ValidatorMessages.DefaultValidatorMessage,
                    HttpStatusCode.UnprocessableEntity));
        }
    }
}