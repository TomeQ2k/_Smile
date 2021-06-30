using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Auth;
using Smile.Infrastructure.Shared.Specifications;
using Smile.Core.Common.Enums;

namespace Smile.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IRolesService rolesService;
        private readonly IUserService userService;

        public AdminService(IDatabase database, IReadOnlyProfileService profileService, IRolesService rolesService, IUserService userService)
        {
            this.database = database;
            this.profileService = profileService;
            this.rolesService = rolesService;
            this.userService = userService;
        }

        public async Task<bool> AdmitRole(string userId, RoleName role)
        {
            var currentAdmin = await GetCurrentAdmin(userId);

            var user = await GetUserToManage(userId);

            return await rolesService.AdmitRole(role, user) ? await database.Complete() : false;
        }

        public async Task<bool> RevokeRole(string userId, RoleName role)
        {
            var currentAdmin = await GetCurrentAdmin(userId);

            var user = await GetUserToManage(userId);

            return await rolesService.RevokeRole(role, user) ? await database.Complete() : false;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var currentAdmin = await GetCurrentAdmin(userId);

            var user = await GetUserToManage(userId);

            database.UserRepository.Delete(user);

            return await database.Complete();
        }

        public async Task<bool> BlockAccount(string userId)
        {
            var currentAdmin = await GetCurrentAdmin(userId);

            var user = await GetUserToManage(userId);

            user.ToggleBlock();

            await database.Complete();

            return user.IsBlocked;
        }

        public async Task<bool> ConfirmAccount(string userId)
        {
            var user = await GetUserToManage(userId);

            if (UserConfirmedSpecification.Create().IsSatisfied(user))
                return false;

            var currentAdmin = GetCurrentAdmin(userId);

            user.ConfirmAccount();

            return await database.Complete();
        }

        #region private

        private async Task<User> GetUserToManage(string userId)
        {
            var user = await userService.GetUser(userId);
            var currentAdmin = await GetCurrentAdmin(userId);

            if (!CanUserBeManagedSpecification.Create(currentAdmin, rolesService).IsSatisfied(user))
                throw new NoPermissionsException("You do not have permissions to manage admin account");

            return user;
        }

        private async Task<User> GetCurrentAdmin(string userId)
        {
            var currentAdmin = await profileService.GetCurrentUser();

            if (!currentAdmin.IsAdmin())
                throw new NoPermissionsException("You are not allowed to perform this action");

            if (currentAdmin.Id == userId)
                throw new NoPermissionsException("You do not have permissions to manage your account");

            return currentAdmin;
        }

        #endregion
    }
}