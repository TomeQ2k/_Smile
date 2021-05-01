using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.Services
{
    public interface IReportManager
    {
        Task<Report> CreateReport(string subject, string content, IEnumerable<IFormFile> files = null);
    }
}