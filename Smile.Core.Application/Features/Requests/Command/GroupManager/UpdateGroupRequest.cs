using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Features.Responses.Command.GroupManager;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.GroupManager
{
    public class UpdateGroupRequest : IRequest<UpdateGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }

        [Required]
        [StringLength(Constants.TitleLength)]
        public string Name { get; set; }

        [StringLength(Constants.ContentLength)]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        [MaxFileSizeValidator(Constants.MaxFileSize)]
        [Ignore]
        public IFormFile Image { get; set; }

        [StringLength(Constants.GroupCodeLength)]
        public string JoinCode { get; set; }

        [Ignore]
        public bool ChangeImage { get; set; }

        public InviteMemberPermission InviteMemberPermission { get; set; }

        public RemoveMemberPermission RemoveMemberPermission { get; set; }
    }
}