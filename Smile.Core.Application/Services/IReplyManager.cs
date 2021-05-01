using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Services
{
    public interface IReplyManager
    {
        Task<Reply> SendReply(string content, string reportId, IEnumerable<IFormFile> files = null);
    }
}