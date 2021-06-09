using MediatR;
using Smile.Core.Application.Features.Responses.Query.Community;

namespace Smile.Core.Application.Features.Requests.Query.Community
{
    public class GetFriendsPaginationRequest : PaginationRequest, IRequest<GetFriendsPaginationResponse>
    {
        public string FriendName { get; set; }

        public string UserId { get; set; }
    }
}