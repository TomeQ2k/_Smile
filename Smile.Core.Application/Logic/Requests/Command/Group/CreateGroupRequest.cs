using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Logic.Responses.Command.Group;
using Smile.Core.Application.Validators;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Group
{
    public class CreateGroupRequest : IRequest<CreateGroupResponse>
    {
        [Required]
        [StringLength(Constants.TitleLength)]
        public string Name { get; set; }

        [StringLength(Constants.ContentLength)]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        [MaxFileSizeValidator(Constants.MaxFileSize)]
        public IFormFile Image { get; set; }

        [StringLength(Constants.GroupCodeLength)]
        public string JoinCode { get; set; }

        public InviteMemberPermission InviteMemberPermission { get; set; } = InviteMemberPermission.Admin;

        public RemoveMemberPermission RemoveMemberPermission { get; set; } = RemoveMemberPermission.Admin;
    }
}