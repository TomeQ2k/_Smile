using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Notification;

namespace Smile.Core.Application.Logic.Requests.Command.Notification
{
    public class MarkNotificationsAsReadRequest : IRequest<MarkNotificationsAsReadResponse>
    { }
}