using System.Collections.Generic;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Results
{
    public class UserGroupsResult
    {
        public List<Group> OwnGroups { get; }
        public List<Group> MyGroups { get; }

        public UserGroupsResult(List<Group> ownGroups, List<Group> myGroups)
        {
            OwnGroups = ownGroups;
            MyGroups = myGroups;
        }
    }
}