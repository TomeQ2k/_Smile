using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Helpers;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Shared.Services
{
    public class ReplyManager : ReportBaseManager, IReplyManager
    {
        private readonly IEmailSender emailSender;

        public ReplyManager(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService,
            IEmailSender emailSender)
            : base(database, filesManager, profileService)
        {
            this.emailSender = emailSender;
        }

        public async Task<Reply> SendReply(string content, string reportId, IEnumerable<IFormFile> files = null)
        {
            var currentUser = await profileService.GetCurrentUser();

            var report = await GetReport(reportId);

            if (report.IsClosed)
                throw new NoPermissionsException("This report is closed");

            bool isAdmin = currentUser.IsAdmin();

            var reply = Reply.Create(content, isAdmin, report);

            if (report.ReporterId == null && !isAdmin)
                throw new NoPermissionsException("You are not allowed to write this reply");
            else if (report.ReporterId == null && isAdmin)
                await emailSender.Send(EmailMessages.ReplyEmail(report.Email, content, report.Subject));
            else
                ValidatePermissions(currentUser, report, isAdmin, reply);

            report.Replies.Add(reply);

            report.Update();

            if (!await database.Complete())
                return null;

            await AttachFiles(files, report, reply);

            return reply;
        }

        #region private

        private async Task AttachFiles(IEnumerable<IFormFile> files, Report report, Reply reply)
        {
            if (files != null)
            {
                var filesUploaded = await filesManager.Upload(files, $"replies/{report.Id}");

                filesUploaded.ToList().ForEach(f => reply.ReplyFiles.Add(ReplyFile.Create<ReplyFile>(f.Path)));

                await database.Complete();
            }
        }

        private static void ValidatePermissions(User currentUser, Report report, bool isAdmin, Reply reply)
        {
            if (report.ReporterId != currentUser.Id && !isAdmin)
                throw new NoPermissionsException("This is not your report");

            if (!reply.IsAdmin && report.Replies.OrderByDescending(r => r.DateSent)
                    .TakeWhile(r => !r.IsAdmin && r.DateSent.AddDays(1) >= DateTime.Now).Count() >=
                Constants.MaxRepliesPerDay)
                throw new NoPermissionsException(
                    $"You are allowed to send only {Constants.MaxRepliesPerDay} replies per day");
        }

        #endregion
    }
}