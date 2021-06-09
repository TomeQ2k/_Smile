using MediatR;
using Smile.Core.Application.Features.Responses.Query.Notification;

namespace Smile.Core.Application.Features.Requests.Query.Notification
{
    public class FetchNotificationsRequest : IRequest<FetchNotificationsResponse>
    { }
}