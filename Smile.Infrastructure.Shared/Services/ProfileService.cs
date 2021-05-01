using Microsoft.AspNetCore.Http;
using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IDatabase database;
        private readonly IFilesManager filesManager;
        private readonly IHttpContextReader httpContextReader;
        private readonly IHashGenerator hashGenerator;

        public ProfileService(IDatabase database, IFilesManager filesManager, IHttpContextReader httpContextReader,
            IHashGenerator hashGenerator)
        {
            this.database = database;
            this.filesManager = filesManager;
            this.httpContextReader = httpContextReader;
            this.hashGenerator = hashGenerator;
        }

        public async Task<User> GetCurrentUser()
            => await database.UserRepository.Get(httpContextReader.CurrentUserId) ??
               throw new EntityNotFoundException("User not found");

        public async Task<bool> ChangeUsername(string newUsername)
        {
            var user = await GetCurrentUser();

            user.SetUsername(newUsername);

            database.UserRepository.Update(user);

            return await database.Complete();
        }

        public async Task<ChangePasswordResult> ChangePassword(string oldPassword, string newPassword)
        {
            var user = await GetCurrentUser();

            if (!hashGenerator.VerifyHash(oldPassword, user.PasswordHash, user.PasswordSalt))
                return new ChangePasswordResult(message: "Old password is invalid");

            string saltedPasswordHash = string.Empty;
            var passwordSalt = hashGenerator.CreateSalt();

            hashGenerator.GenerateHash(newPassword, passwordSalt, out saltedPasswordHash);

            user.SetPassword(saltedPasswordHash, passwordSalt);

            return await database.Complete() ? new ChangePasswordResult(hasChanged: true) : null;
        }

        public async Task<bool> ChangeEmail(string userId, string newEmail, string token)
        {
            var user = await GetCurrentUser();

            if (user.Id != userId)
                throw new NoPermissionsException("You are not allowed to perform this action");

            var changeEmailToken =
                user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.ChangeEmail) ??
                throw new EntityNotFoundException("Token not found");

            user.SetEmail(newEmail);

            database.UserRepository.Update(user);
            database.TokenRepository.Delete(changeEmailToken);

            return await database.Complete();
        }

        public async Task<GenerateChangeEmailTokenResult> GenerateChangeEmailToken(string newEmail)
        {
            var user = await GetCurrentUser();

            var changeEmailToken = Token.Create(TokenType.ChangeEmail);
            changeEmailToken.SetExpirationDate(TimeSpan.FromHours(Constants.TokenExpirationTimeInHours));

            user.Tokens.Add(changeEmailToken);

            return await database.Complete()
                ? new GenerateChangeEmailTokenResult(user.Id, changeEmailToken.Code, newEmail)
                : null;
        }

        public async Task<string> SetAvatar(IFormFile photo)
        {
            var user = await GetCurrentUser();

            string filesPath = $"files/avatars/{user.Id}";
            filesManager.DeleteDirectory(filesPath);

            await database.FileRepository.DeleteFileByPath(filesPath);

            var uploadedAvatar = await filesManager.Upload(photo, $"avatars/{user.Id}");
            user.SetAvatar(uploadedAvatar?.Url);

            database.FileRepository.AddFile(uploadedAvatar?.Url, uploadedAvatar?.Path);

            await database.Complete();

            return user.PhotoUrl;
        }

        public async Task<bool> DeleteAvatar()
        {
            var user = await GetCurrentUser();

            if (string.IsNullOrEmpty(user.PhotoUrl))
                return false;

            string filesPath = $"files/avatars/{user.Id}";
            filesManager.DeleteDirectory(filesPath);
            user.SetAvatar(null);

            await database.FileRepository.DeleteFileByPath(filesPath);

            return await database.Complete();
        }
    }
}