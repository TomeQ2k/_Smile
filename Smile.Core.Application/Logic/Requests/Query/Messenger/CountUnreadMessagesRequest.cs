using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Messenger;

namespace Smile.Core.Application.Logic.Requests.Query.Messenger
{
    public class CountUnreadMessagesRequest : IRequest<CountUnreadMessagesResponse>
    { }
}