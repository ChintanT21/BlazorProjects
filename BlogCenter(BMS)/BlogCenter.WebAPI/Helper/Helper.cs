using BlogCenter.WebAPI.Dtos.ResponceDto;
using System.Text.Json;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;

namespace BlogCenter.WebAPI.Dtos.Helper
{
    public class Helper
    {   
        public BlogStatus ConvertToBlogStatus(int statusValue)
        {
            return Enum.IsDefined(typeof(BlogStatus), statusValue)
                ? (BlogStatus)statusValue
                : throw new ArgumentOutOfRangeException(nameof(statusValue), "Invalid status value");
        }
        public static long GetInt64(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt64();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        public static int GetInt32(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt32();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        public static short GetInt16(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt16();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        public static string GetString(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                return prop.GetString() ?? string.Empty;
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a string.");
        }

        public static bool GetBoolean(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.True || prop.ValueKind == JsonValueKind.False)
            {
                return prop.GetBoolean();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a boolean.");
        }

        public static DateTime GetDateTime(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                return DateTime.Parse(prop.GetString() ?? "2024-07-19T09:55:41");
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a string.");
        }

        public static DateTime? GetNullableDateTime(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                var dateString = prop.GetString();
                return !string.IsNullOrEmpty(dateString) ? (DateTime?)DateTime.Parse(dateString) : null;
            }
            return null;
        }

        public static long? GetNullableInt64(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt64();
            }
            return null;
        }
    }
}
