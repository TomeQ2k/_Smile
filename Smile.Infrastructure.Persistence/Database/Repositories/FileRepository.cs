using Smile.Core.Domain.Data.Repositories;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.File;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context) { }

        public void AddFile(string url, string path)
        {
            var fileToAdd = File.Create<File>(url, path);

            Add(fileToAdd);
        }

        public async Task DeleteFileByPath(string path)
        {
            var fileToDelete = await Find(f => f.Path.ToLower().Contains(path.ToLower()));

            if (fileToDelete != null)
                Delete(fileToDelete);
        }
    }
}