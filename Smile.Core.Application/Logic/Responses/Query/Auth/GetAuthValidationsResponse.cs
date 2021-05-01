using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Auth
{
    public class GetAuthValidationsResponse : BaseResponse
    {
        public bool IsAvailable { get; set; }

        public GetAuthValidationsResponse(Error error = null) : base(error) { }
    }
}