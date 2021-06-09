using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Features.Requests.Query.Community;
using Smile.Core.Application.Features.Responses.Query.Community;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Community
{
    public class GetFriendsQuery : IRequestHandler<GetFriendsPaginationRequest, GetFriendsPaginationResponse>
    {
        private readonly IReadOnlyFriendService friendService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public GetFriendsQuery(IReadOnlyFriendService friendService, IMapper mapper,
            IHttpContextService httpContextService)
        {
            this.friendService = friendService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<GetFriendsPaginationResponse> Handle(GetFriendsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            request.UserId = httpContextService.CurrentUserId;

            var friends = await friendService.GetFriends(request);

            httpContextService.AddPagination(friends.CurrentPage, friends.PageSize, friends.TotalCount,
                friends.TotalPages);

            return new GetFriendsPaginationResponse {Friends = mapper.Map<List<FriendDto>>(friends)};
        }
    }
}