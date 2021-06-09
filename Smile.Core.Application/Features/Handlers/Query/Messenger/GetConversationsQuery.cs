using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Features.Requests.Query.Messenger;
using Smile.Core.Application.Features.Responses.Query.Messenger;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Messenger
{
    public class
        GetConversationsQuery : IRequestHandler<GetConversationsPaginationRequest, GetConversationsPaginationResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        private readonly IMapper mapper;
        private readonly IHttpContextWriter httpContextWriter;

        public GetConversationsQuery(IReadOnlyMessenger messenger, IMapper mapper, IHttpContextWriter httpContextWriter)
        {
            this.messenger = messenger;
            this.mapper = mapper;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetConversationsPaginationResponse> Handle(GetConversationsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            var conversations = await messenger.GetConversations(request);

            httpContextWriter.AddPagination(conversations.CurrentPage, conversations.PageSize, conversations.TotalCount,
                conversations.TotalPages);

            return new GetConversationsPaginationResponse {Conversations = conversations};
        }
    }
}