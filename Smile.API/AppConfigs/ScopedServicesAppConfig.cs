using Microsoft.Extensions.DependencyInjection;
using Smile.API.BackgroundServices;
using Smile.API.BackgroundServices.Interfaces;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.SignalR;
using Smile.Core.Domain.Data;
using Smile.Core.Domain.Data.Mongo;
using Smile.Infrastructure.Persistence.Database;
using Smile.Infrastructure.Persistence.Logging;
using Smile.Infrastructure.Persistence.Mongo;
using Smile.Infrastructure.Shared.Services;
using Smile.Infrastructure.Shared.SignalR;

namespace Smile.API.AppConfigs
{
    public static class ScopedServicesAppConfig
    {
        public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabase, Database>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IMessenger, Messenger>();
            services.AddScoped<IStoryManager, StoryManager>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISupportService, SupportService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IResetPasswordManager, ResetPasswordManager>();
            services.AddScoped<IReportBaseManager, ReportBaseManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IReportAnonymousManager, ReportAnonymousManager>();
            services.AddScoped<IReplyManager, ReplyManager>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<ILogManager, LogManager>();
            services.AddScoped<IJwtAuthorizationTokenGenerator, JwtAuthorizationTokenGenerator>();
            services.AddScoped<IHubManager, HubManager>();
            services.AddScoped<IConnectionManager, ConnectionManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IAuthValidationService, AuthValidationService>();

            services.AddScoped<IReadOnlyRolesService, RolesService>();
            services.AddScoped<IReadOnlyPostService, PostService>();
            services.AddScoped<IReadOnlyFriendService, FriendService>();
            services.AddScoped<IReadOnlyProfileService, ProfileService>();
            services.AddScoped<IReadOnlyMessenger, Messenger>();
            services.AddScoped<IReadOnlyStoryManager, StoryManager>();
            services.AddScoped<IReadOnlyGroupService, GroupService>();
            services.AddScoped<IReadOnlyGroupManager, GroupManager>();
            services.AddScoped<IReadOnlyNotifier, Notifier>();
            services.AddScoped<IReadOnlyResetPasswordManager, ResetPasswordManager>();
            services.AddScoped<IReadOnlyLogManager, LogManager>();

            return services;
        }
    }
}