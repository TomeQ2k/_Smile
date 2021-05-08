using Microsoft.AspNetCore.Http;
using Smile.Core.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Models.Story;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Infrastructure.Shared.Services
{
    public class StoryManager : IStoryManager
    {
        private readonly IDatabase database;
        private readonly IFilesManager filesManager;
        private readonly IReadOnlyProfileService profileService;

        public StoryManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService)
        {
            this.database = database;
            this.filesManager = filesManager;
            this.profileService = profileService;
        }

        public async Task<IEnumerable<Story>> FetchStories()
        {
            var currentUser = await profileService.GetCurrentUser();

            var friends = currentUser.FriendsSent.Concat(currentUser.FriendsReceived);

            return currentUser.Stories.Where(s => s.DateExpires >= DateTime.Now).OrderByDescending(s => s.DateExpires)
                .Concat(await database.StoryRepository.GetUserCurrentStories(currentUser.Id))
                .Distinct();
        }

        public async Task<Story> AddStory(IFormFile photo)
        {
            var currentUser = await profileService.GetCurrentUser();

            var story = new Story();

            currentUser.Stories.Add(story);

            if (!await database.Complete())
                return null;

            var uploadedPhoto = await filesManager.Upload(photo, $"stories/{story.Id}");
            story.SetStoryUrl(uploadedPhoto?.Path);

            database.FileRepository.AddFile(uploadedPhoto?.Path);

            return await database.Complete() ? story : null;
        }

        public async Task<bool> WatchStory(string storyId, string userId)
        {
            var story = await database.StoryRepository.Get(storyId) ??
                        throw new EntityNotFoundException("Story not found");

            if (story.UserStories.Any(us => us.UserId == userId))
                return true;

            var userStory = UserStory.Create(story.Id, userId);

            story.UserStories.Add(userStory);

            return await database.Complete();
        }

        public async Task<bool> DeleteStory(string storyId)
        {
            var story = (await profileService.GetCurrentUser()).Stories.FirstOrDefault(s => s.Id == storyId)
                        ?? throw new EntityNotFoundException("Story not found");

            database.StoryRepository.Delete(story);

            string filesPath = $"files/stories/{story.Id}";
            filesManager.DeleteDirectory(filesPath);

            await database.FileRepository.DeleteFileByPath(filesPath);

            return await database.Complete();
        }

        public IEnumerable<StoryWrapper> CreateStoryWrappers(string currentUserId, List<StoryDto> stories)
            => stories.GroupBy(s => s.UserId)
                .Select(x =>
                {
                    var story = x.First();
                    var storiesToReturn = stories.Where(s => s.UserId == story.UserId).ToList();

                    storiesToReturn.ForEach(story => story.IsWatched = IsWatched(story, currentUserId));

                    var storyWrapper =
                        new StoryWrapper(story.UserId, story.Username, story.UserPhotoUrl, storiesToReturn);
                    storyWrapper.SetIsWatched();

                    return storyWrapper;
                });

        public async Task ClearStories()
        {
            var storiesToDelete = await database.StoryRepository.GetWhere(s => s.DateExpires < DateTime.Now);

            database.StoryRepository.DeleteRange(storiesToDelete);

            await database.Complete();

            foreach (var story in storiesToDelete)
            {
                string filesPath = $"files/stories/{story.Id}";
                filesManager.DeleteDirectory(filesPath);

                await database.FileRepository.DeleteFileByPath(filesPath);
            }

            await database.Complete();
        }

        #region private

        private static bool IsWatched(StoryDto story, string currentUserId)
            => story.UserStories.Any(us => us.UserId == currentUserId);

        #endregion
    }
}