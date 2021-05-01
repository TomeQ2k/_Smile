using System;

namespace Smile.Core.Application.Logging
{
    public interface INLogger
    {
        void Info(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
        void Error(string message, Exception exception);
    }
}