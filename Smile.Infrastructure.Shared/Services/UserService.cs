using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logic.Requests.Query.User;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;

        public EmailConfirmedStatusSmartEnum EmailConfirmedStatus { get; set; }

        public UserService(IDatabase database, IReadOnlyProfileService profileService)
        {
            this.database = database;
            this.profileService = profileService;
        }

        public async Task<User> GetUser(string userId, string currentUserId = null)
            => await database.UserRepository.Find(u => u.Id == userId && u.Id != currentUserId) ??
               throw new EntityNotFoundException("User not found");

        public async Task<PagedList<User>> GetUsers(GetUsersPaginationRequest paginationRequest)
        {
            var users = await database.UserRepository.GetFilteredUsers(paginationRequest);

            if (paginationRequest.OnlyAdmin)
                users = users.Where(u => u.IsAdmin());

            users = UserSortTypeSmartEnum.FromValue((int) paginationRequest.SortType).Sort(users);

            return users.ToPagedList<User>(paginationRequest.PageNumber, paginationRequest.PageSize);
        }
    }
}