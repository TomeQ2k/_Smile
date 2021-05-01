using MediatR;
using Microsoft.AspNetCore.Mvc;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator mediator;
        protected readonly INLogger logger;

        public BaseController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
    }
}