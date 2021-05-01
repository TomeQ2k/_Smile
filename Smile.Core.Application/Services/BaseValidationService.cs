using Smile.Core.Domain.Data;

namespace Smile.Core.Application.Services
{
    public abstract class BaseValidationService
    {
        protected readonly IDatabase database;

        public BaseValidationService(IDatabase database)
        {
            this.database = database;
        }
    }
}