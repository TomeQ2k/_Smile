using System.Collections.Generic;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Group
{
    public class FetchUserGroupsResponse : BaseResponse
    {
        public List<GroupListDto> OwnGroups { get; set; }
        public List<GroupListDto> MyGroups { get; set; }

        public FetchUserGroupsResponse(Error error = null) : base(error) { }
    }
}