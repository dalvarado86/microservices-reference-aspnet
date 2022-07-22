using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Commons.Utils
{
    public static class JsonSettings
    {
        public static readonly JsonSerializerSettings DefaultNewtonJsonSetting = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                },
            },
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static readonly JsonSerializerOptions DefaultTextJsonSetting = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            Converters =
            {
                new JsonStringEnumMemberConverter(new SnakeCaseNamingPolicy()),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
}
