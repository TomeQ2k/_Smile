using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Notification;

namespace Smile.Core.Application.Logic.Requests.Query.Notification
{
    public class FetchNotificationsRequest : IRequest<FetchNotificationsResponse>
    { }
}