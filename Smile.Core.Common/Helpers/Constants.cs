using Smile.Core.Common.Enums;

namespace Smile.Core.Common.Helpers
{
    public static class Constants
    {
        #region values

        public const int MinUsernameLength = 5;
        public const int MaxUsernameLength = 24;
        public const int MinPasswordLength = 5;
        public const int MaxPasswordLength = 30;
        public const int TitleLength = 150;
        public const int ContentLength = 1500;
        public const int CommentLength = 500;
        public const int MessageLength = 250;

        public const int MaxFilesCount = 5;
        public const int MaxFileSize = 3;

        public const int GroupCodeLength = 16;

        public const int TokenExpirationTimeInHours = 1;

        public const int ServerHostedServiceTimeInMinutes = 1440;

        public const int UnitConversionMultiplier = 1024;

        public const string NlogConfig = "nlog.config";


        public const string INFO = "INFO";
        public const string DEBUG = "DEBUG";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";

        public const int InfoLogLifeTimeInMonths = 1;
        public const int WarningLogLifeTimeInMonths = 2;
        public const int ErrorLogLifeTimeInMonths = 3;

        public const int MaxRepliesPerDay = 3;

        public const int PageSize = 10;
        public const int LogsPageSize = 50;

        #endregion

        #region policies

        public const string AdminPolicy = "AdminPolicy";
        public const string HeadAdminPolicy = "HeadAdminPolicy";

        public const string CorsPolicy = "CorsPolicy";

        #endregion

        #region roles

        public static string AdminRole = "Admin";
        public static string HeadAdminRole = "HeadAdmin";

        public static string[] AdminRoles = { AdminRole, HeadAdminRole };
        public static RoleName[] SupportRoles = { RoleName.Admin, RoleName.HeadAdmin };
        public static RoleName[] RolesToSeed = { RoleName.User, RoleName.Admin, RoleName.HeadAdmin };

        #endregion
    }
}