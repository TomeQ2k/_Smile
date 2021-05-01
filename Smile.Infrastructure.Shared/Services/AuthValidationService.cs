using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Services;

namespace Smile.Infrastructure.Shared.Services
{
    public class AuthValidationService : BaseValidationService, IAuthValidationService
    {
        public AuthValidationService(IDatabase database) : base(database) { }

        public async Task<bool> EmailExists(string email)
            => await database.UserRepository.Find(u => u.Email.ToLower() == email.ToLower()) != null;

        public async Task<bool> UsernameExists(string username)
            => await database.UserRepository.Find(u => u.Username.ToLower() == username.ToLower()) != null;
    }
}