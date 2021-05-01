using Smile.Core.Application.Logging;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogResponse(this INLogger logger, string message, Error error = null)
        {
            if (error != null)
                logger.Error(error.Message);
            else
                logger.Info(message);
        }
    }
}