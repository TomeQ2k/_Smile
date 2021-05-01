using System;
using Smile.Core.Application.Attributes;
using Smile.Core.Domain.Data.Mongo;

namespace Smile.Core.Application.Models.Mongo
{
    [BsonCollection("logs")]
    public class LogDocument : Document
    {
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
    }
}