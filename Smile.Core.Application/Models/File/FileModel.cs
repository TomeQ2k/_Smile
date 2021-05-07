namespace Smile.Core.Application.Models.File
{
    public class FileModel
    {
        public string Path { get; }
        public string Url { get; }
        public string FullPath { get; }

        public FileModel(string path, string url, string fullPath = null)
        {
            Path = path;
            Url = url;
            FullPath = fullPath;
        }
    }
}