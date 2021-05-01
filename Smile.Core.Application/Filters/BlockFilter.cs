using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Extensions;
using Smile.Core.Domain.Data;

namespace Smile.Core.Application.Filters
{
    public class BlockFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext result;

            string currentUserId = context.HttpContext.GetCurrentUserId();

            if (currentUserId == null)
            {
                result = await next();
                return;
            }

            var database = context.HttpContext.RequestServices.GetService<IDatabase>();

            var currentUser = await database.UserRepository.Get(currentUserId) ?? throw new EntityNotFoundException("User not found");

            if (currentUser.IsBlocked)
                throw new BlockException("Your account is blocked");

            result = await next();
        }
    }
}