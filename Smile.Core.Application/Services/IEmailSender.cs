using System.Threading.Tasks;
using Smile.Core.Application.Models.Email;

namespace Smile.Core.Application.Services
{
    public interface IEmailSender
    {
        Task<bool> Send(EmailMessage emailMessage);
    }
}