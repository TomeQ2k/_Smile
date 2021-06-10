using Smile.API.BackgroundServices.Interfaces;
using Smile.Core.Common.Helpers;
using Smile.Core.Application.Services;
using System.Threading.Tasks;

namespace Smile.API.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesService rolesService;

        public DatabaseManager(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public async Task Seed()
        {
            await InsertRoles();
        }

        #region private

        private async Task InsertRoles()
        {
            foreach (var roleName in Constants.RolesToSeed)
                await rolesService.CreateRole(roleName);
        }

        #endregion
    }
}