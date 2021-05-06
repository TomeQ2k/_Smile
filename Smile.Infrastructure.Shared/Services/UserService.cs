using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.User;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Data.Models;

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

        public async Task<IPagedList<User>> GetUsers(GetUsersPaginationRequest paginationRequest)
            => await database.UserRepository.GetFilteredUsers(paginationRequest, (paginationRequest.PageNumber, paginationRequest.PageSize));
    }
}