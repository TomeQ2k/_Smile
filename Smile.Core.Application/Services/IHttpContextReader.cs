namespace Smile.Core.Application.Services
{
    public interface IHttpContextReader
    {
        string CurrentUserId { get; }
    }
}