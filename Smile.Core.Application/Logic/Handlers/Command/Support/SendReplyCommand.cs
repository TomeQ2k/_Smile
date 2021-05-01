using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Support;
using Smile.Core.Application.Logic.Responses.Command.Support;
using Smile.Core.Application.Services;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Command.Support
{
    public class SendReplyCommand : IRequestHandler<SendReplyRequest, SendReplyResponse>
    {
        private readonly IReplyManager replyManager;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHttpContextReader httpContextReader;

        public SendReplyCommand(IReplyManager replyManager, IMapper mapper, INotifier notifier,
            IHttpContextReader httpContextReader)
        {
            this.replyManager = replyManager;
            this.mapper = mapper;
            this.notifier = notifier;
            this.httpContextReader = httpContextReader;
        }

        public async Task<SendReplyResponse> Handle(SendReplyRequest request, CancellationToken cancellationToken)
        {
            var reply = await replyManager.SendReply(request.Content, request.ReportId, request.Files) ??
                        throw new CrudException("Reply sending failed");

            if (reply.Report.ReporterId != null && httpContextReader.CurrentUserId != reply.Report.ReporterId)
                await notifier.Push(NotificationMessages.SupportReplyNotification(reply.Report.Subject),
                    reply.Report.ReporterId, NotificationType.SupportReply);

            return new SendReplyResponse {Reply = mapper.Map<ReplyDto>(reply)};
        }
    }
}