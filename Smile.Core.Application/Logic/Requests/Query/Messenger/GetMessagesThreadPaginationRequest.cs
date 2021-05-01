using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Messenger;

namespace Smile.Core.Application.Logic.Requests.Query.Messenger
{
    public class GetMessagesThreadPaginationRequest : PaginationRequest, IRequest<GetMessagesThreadPaginationResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}