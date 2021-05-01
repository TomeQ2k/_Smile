using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Smile.Core.Application.Settings
{
    public static class JsonSettings
    {
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
}