using MediatR;
using Smile.Core.Application.Features.Responses.Query.Messenger;

namespace Smile.Core.Application.Features.Requests.Query.Messenger
{
    public class GetConversationsPaginationRequest : PaginationRequest, IRequest<GetConversationsPaginationResponse>
    {
        public string Username { get; set; }
    }
}