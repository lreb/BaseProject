using System;
using System.Text.Json;

namespace BaseProjectAPI.Service.Utilities
{
    public static class Utilities
    {
        public static T DeserializeJson<T>(string json)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(json, typeof(T));

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            return JsonSerializer.Deserialize<T>(json, serializeOptions);
        }
    }
}
