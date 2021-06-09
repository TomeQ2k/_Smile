using MediatR;
using Smile.Core.Application.Features.Responses.Command.Notification;

namespace Smile.Core.Application.Features.Requests.Command.Notification
{
    public class MarkNotificationsAsReadRequest : IRequest<MarkNotificationsAsReadResponse>
    { }
}