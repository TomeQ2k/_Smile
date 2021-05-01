using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.User;
using Smile.Core.Application.Logic.Requests.Query.User;
using Smile.Core.Application.Logic.Responses.Query.User;
using Smile.Core.Application.Services;
using Smile.Core.Application.ServiceUtils;

namespace Smile.Core.Application.Logic.Handlers.Query.User
{
    public class GetUsersQuery : IRequestHandler<GetUsersPaginationRequest, GetUsersPaginationResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public GetUsersQuery(IUserService userService, IMapper mapper, IHttpContextService httpContextService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<GetUsersPaginationResponse> Handle(GetUsersPaginationRequest request,
            CancellationToken cancellationToken)
        {
            string currentUserId = httpContextService.CurrentUserId;

            request.UserId = currentUserId;

            var users = await userService.GetUsers(request);

            httpContextService.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            var usersToReturn = mapper.Map<List<UserDto>>(users);

            for (int i = 0; i < users.Count; i++)
                usersToReturn[i] = (UserDto) FriendUtils.SetFriendProperties(usersToReturn[i], users[i], currentUserId);

            return new GetUsersPaginationResponse {Users = usersToReturn};
        }
    }
}