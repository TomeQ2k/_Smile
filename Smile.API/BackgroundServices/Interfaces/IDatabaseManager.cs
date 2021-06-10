using System.Threading.Tasks;

namespace Smile.API.BackgroundServices.Interfaces
{
    public interface IDatabaseManager
    {
        Task Seed();
    }
}