using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Services
{
    public interface IProfileService : IReadOnlyProfileService
    {
        Task<bool> ChangeUsername(string newUsername);
        Task<ChangePasswordResult> ChangePassword(string oldPassword, string newPassword);
        Task<bool> ChangeEmail(string userId, string newEmail, string token);

        Task<GenerateChangeEmailTokenResult> GenerateChangeEmailToken(string newEmail);

        Task<string> SetAvatar(IFormFile photo);
        Task<bool> DeleteAvatar();
    }
}