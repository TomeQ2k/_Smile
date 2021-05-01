using System.Threading.Tasks;

namespace Smile.Core.Application.SignalR
{
    public interface IConnectionManager
    {
        Task<bool> StartConnection(string connectionId);
        Task<bool> CloseConnection(string connectionId);

        Task<string> GetConnectionId(string userId);
    }
}