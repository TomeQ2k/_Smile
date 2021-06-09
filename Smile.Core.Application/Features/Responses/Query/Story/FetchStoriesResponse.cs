using System.Collections.Generic;
using Smile.Core.Application.Models.Error;
using Smile.Core.Application.Models.Story;

namespace Smile.Core.Application.Features.Responses.Query.Story
{
    public class FetchStoriesResponse : BaseResponse
    {
        public IEnumerable<StoryWrapper> Stories { get; set; }

        public FetchStoriesResponse(Error error = null) : base(error) { }
    }
}