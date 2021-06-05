using System;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.Builders.Interfaces
{
    public interface ILogBuilder : IBuilder<LogDocument>
    {
        ILogBuilder CreatedAt(DateTime date);
        ILogBuilder SetLevel(string level);
        ILogBuilder SetLogger(string logger);
        ILogBuilder SetMessage(string message, string trace);
        ILogBuilder WithAction(string url, string action);
    }
}