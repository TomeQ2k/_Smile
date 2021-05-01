using System.Threading.Tasks;

namespace Smile.Core.Application.Logging
{
    public interface ILogManager : IReadOnlyLogManager
    {
        Task<bool> StoreLogs();
        Task ClearLogs();
    }
}