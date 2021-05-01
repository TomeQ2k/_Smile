using Smile.API.BackgroundServices.Interfaces;
using Smile.Core.Common.Helpers;
using System.Linq;
using Smile.Core.Application.Services;

namespace Smile.API.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesService rolesService;

        public DatabaseManager(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public void Seed()
        {
            InsertRoles();
        }

        #region private

        private void InsertRoles()
        {
            Constants.RolesToSeed.ToList().ForEach((roleName) =>
            {
                rolesService.CreateRole(roleName).Wait();
            });
        }

        #endregion
    }
}