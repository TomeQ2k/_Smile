using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smile.Core.Common.Helpers;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Requests.Command.Support;
using Smile.Core.Application.Logic.Requests.Query.Support;

namespace Smile.API.Controllers
{
    public class SupportController : BaseController
    {
        public SupportController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] GetReportRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched report #{response?.Report?.Id}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("reports/user")]
        public async Task<IActionResult> FetchUserReports([FromQuery] FetchUserReportsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched reports", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("reports/all")]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> FetchAllReports([FromQuery] FetchAllReportsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched all reports", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("report/create")]
        public async Task<IActionResult> CreateReport([FromForm] CreateReportRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} created report #{response?.Report?.Id} with subject: " +
                $"{response?.Report?.Subject.ToUpper()}", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("reply/send")]
        public async Task<IActionResult> SendReply([FromForm] SendReplyRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} sent reply #{response?.Reply?.Id} to report #{request.ReportId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("report/anonymous/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAnonymousReport(CreateAnonymousReportRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Someone created anonymous report #{response?.Report?.Id} using email: {request.Email}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("report/toggleStatus")]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> ToggleReportStatus(ToggleReportStatusRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} toggled report #{request.ReportId} status to: {(response.IsClosed ? "CLOSED" : "OPENED")}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("report/delete")]
        [Authorize(Policy = Constants.HeadAdminPolicy)]
        public async Task<IActionResult> DeleteReport([FromQuery] DeleteReportRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted report #{request.ReportId}",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}