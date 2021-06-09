using System.Collections.Generic;

namespace Smile.Core.Application.Models.Validation
{
    public class ValidationError
    {
        public string Field { get; }
        public IEnumerable<string> Messages { get; }

        public ValidationError(string field, IEnumerable<string> messages)
        {
            Field = field != string.Empty ? field : null;
            Messages = messages;
        }
    }
}