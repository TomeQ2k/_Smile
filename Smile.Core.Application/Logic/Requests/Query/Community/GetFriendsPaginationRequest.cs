using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Community;

namespace Smile.Core.Application.Logic.Requests.Query.Community
{
    public class GetFriendsPaginationRequest : PaginationRequest, IRequest<GetFriendsPaginationResponse>
    {
        public string FriendName { get; set; }

        public string UserId { get; set; }
    }
}