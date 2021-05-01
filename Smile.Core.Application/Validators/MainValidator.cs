using Microsoft.AspNetCore.Mvc.Filters;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Models.Error;
using Smile.Core.Application.Models.Validation;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Validators
{
    public class MainValidator : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState, Error.Build(ErrorCodes.ValidationError, ValidatorMessages.MainValidatorMessage));
        }
    }
}