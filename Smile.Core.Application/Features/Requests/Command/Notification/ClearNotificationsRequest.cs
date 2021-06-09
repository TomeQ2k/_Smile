using MediatR;
using Smile.Core.Application.Features.Responses.Command.Notification;

namespace Smile.Core.Application.Features.Requests.Command.Notification
{
    public class ClearNotificationsRequest : IRequest<ClearNotificationsResponse>
    { }
}