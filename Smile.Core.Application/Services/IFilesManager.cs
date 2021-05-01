using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Models.File;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Services
{
    public interface IFilesManager : IReadOnlyFilesManager
    {
        Task<FileModel> Upload(IFormFile file, string filePath = null);
        Task<IList<FileModel>> Upload(IEnumerable<IFormFile> files, string filePath = null);

        void Delete(string path);
        void DeleteDirectory(string path, bool isRecursive = true);
        void DeleteByFullPath(string fullPath);
    }
}