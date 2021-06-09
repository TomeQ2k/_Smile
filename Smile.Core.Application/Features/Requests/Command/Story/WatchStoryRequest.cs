using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Story;

namespace Smile.Core.Application.Features.Requests.Command.Story
{
    public class WatchStoryRequest : IRequest<WatchStoryResponse>
    {
        [Required]
        public string StoryId { get; set; }
    }
}