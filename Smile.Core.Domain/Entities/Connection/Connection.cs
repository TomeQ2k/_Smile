using System;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Connection
{
    public class Connection
    {
        public string UserId { get; protected set; }
        public string ConnectionId { get; protected set; }
        public DateTime DateEstablished { get; protected set; } = DateTime.Now;

        public virtual User User { get; protected set; }

        public static Connection Create(string userId, string connId) => new Connection { UserId = userId, ConnectionId = connId };

        public void SetConnectionId(string connectionId)
        {
            ConnectionId = connectionId;
        }
    }
}