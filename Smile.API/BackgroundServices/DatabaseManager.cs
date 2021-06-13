using Smile.API.BackgroundServices.Interfaces;
using Smile.Core.Common.Helpers;
using Smile.Core.Application.Services;
using System.Threading.Tasks;
using Smile.Core.Application.Logging;

namespace Smile.API.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesService rolesService;
        private readonly INLogger logger;

        public DatabaseManager(IRolesService rolesService, INLogger logger)
        {
            this.rolesService = rolesService;
            this.logger = logger;
        }

        public async Task Seed()
        {
            await InsertRoles();

            logger.Info("Database seed completed");
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