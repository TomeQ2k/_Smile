using System;
using AutoMapper;
using Smile.Core.Application.Dtos.Auth;
using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Dtos.Messenger;
using Smile.Core.Application.Dtos.Notification;
using Smile.Core.Application.Dtos.Profile;
using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Dtos.User;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Logic.Requests.Command.Post;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Community;
using Smile.Core.Domain.Entities.Group;
using Smile.Core.Domain.Entities.Main;
using Smile.Core.Domain.Entities.Messenger;
using Smile.Core.Domain.Entities.Notification;
using Smile.Core.Domain.Entities.Story;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(u => u.IsAdmin()))
                 .ForMember(dest => dest.Posts, opt => opt.MapFrom(u => u.GetPostsWithoutGroup()))
                 .ForMember(dest => dest.Groups, opt => opt.MapFrom(u => u.GetUserGroups()));
            CreateMap<User, UserAuthDto>();
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<User, UserAdminDto>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(u => u.IsAdmin()))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(u => u.GetPostsWithoutGroup()))
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(u => u.GetUserGroups()));
            CreateMap<User, RecipientDto>();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(p => p.Comments.Count))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(p => p.Likes.Count))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(p => p.Author.Username));
            CreateMap<CreatePostRequest, Post>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateUpdated, opt => opt.Ignore());
            CreateMap<UpdatePostRequest, Post>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(c => c.User.Username))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(c => c.User.PhotoUrl));

            CreateMap<Like, LikeDto>();

            CreateMap<Friend, FriendDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(f => f.Sender.Username))
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(f => f.Sender.PhotoUrl))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(f => f.Recipient.Username))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(f => f.Recipient.PhotoUrl))
                .ForMember(dest => dest.IsAccepted, opt => opt.MapFrom(f => f.SenderAccepted && f.RecipientAccepted));

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(m => m.Sender.Username))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(m => m.Recipient.Username))
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(m => m.Sender.PhotoUrl))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(m => m.Recipient.PhotoUrl));

            CreateMap<Story, StoryDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(s => s.User.PhotoUrl))
                .ForMember(dest => dest.WatchedByCount, opt => opt.MapFrom(s => s.UserStories.Count));

            CreateMap<UserStory, UserStoryDto>();

            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(r => r.Reporter.Username))
                .ForMember(dest => dest.ReporterPhotoUrl, opt => opt.MapFrom(r => r.Reporter.PhotoUrl))
                .ForMember(dest => dest.IsAnonymous, opt => opt.MapFrom(r => r.ReporterId == null));
            CreateMap<Report, ReportListDto>()
                .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(r => r.Reporter.Username))
                .ForMember(dest => dest.IsAnonymous, opt => opt.MapFrom(r => r.ReporterId == null));

            CreateMap<Reply, ReplyDto>()
                .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(r => r.Report.Reporter.Username));

            CreateMap<ReportFile, AttachmentFileDto>();
            CreateMap<ReplyFile, AttachmentFileDto>();

            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(g => g.Admin.Username))
                .ForMember(dest => dest.AdminPhotoUrl, opt => opt.MapFrom(g => g.Admin.PhotoUrl))
                .ForMember(dest => dest.MembersCount, opt => opt.MapFrom(g => g.GetMembersCount()))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(g => g.GetMembers()))
                .ForMember(dest => dest.Moderators, opt => opt.MapFrom(g => g.GetModerators()));
            CreateMap<Group, GroupListDto>()
                .ForMember(dest => dest.MembersCount, opt => opt.MapFrom(g => g.GetMembersCount()))
                .ForMember(dest => dest.HasCode, opt => opt.MapFrom(g => !string.IsNullOrEmpty(g.JoinCode)));
            CreateMap<UpdateGroupRequest, Group>();

            CreateMap<GroupMember, GroupMemberDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(m => m.User.Username))
                .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(m => m.User.PhotoUrl));

            CreateMap<GroupInvite, GroupInviteDto>();

            CreateMap<Notification, NotificationDto>();
        }
    }
}