using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Features.Requests.Command.Messenger;
using Smile.Core.Application.Features.Requests.Query.Messenger;
using Smile.Core.Application.Logging;

namespace Smile.API.Controllers
{
    public class MessengerController : BaseController
    {
        public MessengerController(IMediator mediator, INLogger logger) : base(mediator, logger)
        {
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations([FromQuery] GetConversationsPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} filtered their conversations", response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessagesThread([FromQuery] GetMessagesThreadPaginationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} opened chat with user #{request.RecipientId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPost("message/send")]
        public async Task<IActionResult> SendMessage(SendMessageRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} sent message to friend #{request.RecipientId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("message/delete")]
        public async Task<IActionResult> DeleteMessage([FromQuery] DeleteMessageRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted message #{request.MessageId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpDelete("conversation/delete")]
        public async Task<IActionResult> DeleteConversation([FromQuery] DeleteConversationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} deleted conversation with user #{request.RecipientId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpPatch("message/read")]
        public async Task<IActionResult> ReadMessage(ReadMessageRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} read message #{request.MessageId}",
                response.Error);

            return this.CreateResponse(response);
        }

        [HttpGet("messages/unread/count")]
        public async Task<IActionResult> CountUnreadMessages([FromQuery] CountUnreadMessagesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse(
                $"User #{HttpContext.GetCurrentUserId()} has {response?.UnreadMessagesCount} unread messages",
                response.Error);

            return this.CreateResponse(response);
        }
    }
}