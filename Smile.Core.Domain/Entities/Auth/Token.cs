using System;
using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Domain.Entities.Auth
{
    public class Token
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Code { get; protected set; } = Utils.Token(length: 30);
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime? DateExpired { get; protected set; }
        public TokenType TokenType { get; protected set; }
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }

        public static Token Create(TokenType tokenType) => new Token { TokenType = tokenType };

        public void SetExpirationDate(TimeSpan timeSpan)
        {
            DateExpired = DateCreated.Add(timeSpan);
        }
    }
}