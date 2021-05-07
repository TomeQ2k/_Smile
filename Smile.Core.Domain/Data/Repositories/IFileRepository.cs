using System.Threading.Tasks;
using Smile.Core.Domain.Entities.File;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IFileRepository : IRepository<File>
    {
        void AddFile(string path);

        Task DeleteFileByPath(string path);
    }
}