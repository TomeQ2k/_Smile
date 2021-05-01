using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.User;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.User;
using Smile.Core.Application.Logic.Responses.Query.User;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.ServiceUtils;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Query.User
{
    public class GetUserQuery : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextReader httpContextReader;
        private readonly IReadOnlyRolesService rolesService;

        public GetUserQuery(IUserService userService, IMapper mapper, IHttpContextReader httpContextReader,
            IReadOnlyRolesService rolesService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextReader = httpContextReader;
            this.rolesService = rolesService;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            string currentUserId = httpContextReader.CurrentUserId;

            var user = await userService.GetUser(request.UserId, currentUserId);

            var userToReturn = !await rolesService.IsPermitted(currentUserId, RoleName.Admin)
                ? (UserDtoBase) mapper.Map<UserDto>(user)
                : mapper.Map<UserAdminDto>(user);

            if (user != null)
            {
                userToReturn = FriendUtils.SetFriendProperties(userToReturn, user, currentUserId);

                user.SortPosts();

                var userGroups = user.Groups.Concat(user.GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group))
                    .ToList();

                GroupUtils.SetGroupMemberParams(userToReturn.Groups.ToList(), userGroups, currentUserId);

                return new GetUserResponse {User = userToReturn};
            }

            throw new CrudException("User cannot be loaded");
        }
    }
}