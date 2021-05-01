using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Story;

namespace Smile.Core.Application.Logic.Requests.Command.Story
{
    public class WatchStoryRequest : IRequest<WatchStoryResponse>
    {
        [Required]
        public string StoryId { get; set; }
    }
}