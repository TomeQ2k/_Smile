using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.Messenger;

namespace Smile.Core.Application.Features.Requests.Query.Messenger
{
    public class GetMessagesThreadPaginationRequest : PaginationRequest, IRequest<GetMessagesThreadPaginationResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}