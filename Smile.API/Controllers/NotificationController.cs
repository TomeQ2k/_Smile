using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.Notification;
using Smile.Core.Application.Features.Requests.Query.Notification;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    public class NotificationController : BaseController
    {
        public NotificationController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> FetchNotifications([FromQuery] FetchNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched their notifications", response.Error);

            return this.CreateResponse(response);
        }

        [HttpPut("markAsRead")]
        public async Task<IActionResult> MarkNotificationsAsRead(MarkNotificationsAsReadRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} marked their notifications as read",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveNotification([FromQuery] RemoveNotificationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} remove notification #{request.NotificationId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearNotifications([FromQuery] ClearNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} cleared their notifications", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("unread/count")]
        public async Task<IActionResult> CountUnreadNotifications([FromQuery] CountUnreadNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} has {response?.UnreadNotificationsCount} unread notifications",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}