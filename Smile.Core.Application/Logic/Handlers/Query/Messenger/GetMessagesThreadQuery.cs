using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Messenger;
using Smile.Core.Application.Logic.Requests.Query.Messenger;
using Smile.Core.Application.Logic.Responses.Query.Messenger;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.Messenger
{
    public class GetMessagesThreadQuery : IRequestHandler<GetMessagesThreadPaginationRequest, GetMessagesThreadPaginationResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        private readonly IMapper mapper;
        private readonly IHttpContextWriter httpContextWriter;

        public GetMessagesThreadQuery(IReadOnlyMessenger messenger, IMapper mapper, IHttpContextWriter httpContextWriter)
        {
            this.messenger = messenger;
            this.mapper = mapper;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetMessagesThreadPaginationResponse> Handle(GetMessagesThreadPaginationRequest request,
            CancellationToken cancellationToken)
        {
            var messages = await messenger.GetMessagesThread(request);

            var messagesToReturn = mapper.Map<List<MessageDto>>(messages);

            httpContextWriter.AddPagination(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            var recipientToReturn = mapper.Map<RecipientDto>(await messenger.GetRecipient(request.RecipientId));

            return new GetMessagesThreadPaginationResponse
            {
                Messages = messagesToReturn,
                Recipient = recipientToReturn
            };
        }
    }
}