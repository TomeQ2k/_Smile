namespace Smile.Core.Application.Results
{
    public class ChangePasswordResult
    {
        public bool HasChanged { get; }
        public string Message { get; }

        public ChangePasswordResult(string message = null, bool hasChanged = false)
        {
            Message = message;
            HasChanged = hasChanged;
        }
    }
}