using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Domain.Entities.File
{
    public abstract class BaseFile
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Url { get; protected set; }
        public string Path { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;

        public static T Create<T>(string url, string path) where T : BaseFile, new() => new T
        {
            Url = url,
            Path = path
        };
    }
}