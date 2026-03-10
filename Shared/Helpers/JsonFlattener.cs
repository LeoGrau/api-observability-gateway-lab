using System.Text.Json;

namespace Shared.Helpers;

public static class JsonFlattener
{
    private static void FlattenElement(JsonElement jsonElement, Dictionary<string, string> result, string prefix)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in jsonElement.EnumerateObject())
                {
                    var lowerAttributeName = property.Name.ToLowerInvariant();
                    var newPrefix = string.IsNullOrWhiteSpace(prefix)
                        ? lowerAttributeName
                        : $"{prefix}.{lowerAttributeName}";
                    FlattenElement(property.Value, result, newPrefix);
                }

                break;
            default:
                result.Add(prefix, jsonElement.ToString());
                break;
        }
    }
    
    public static Dictionary<string, string> Flatten(string json)
    {
        var result = new Dictionary<string, string>();
        using var jsonDoc = JsonDocument.Parse(json);
        FlattenElement(jsonDoc.RootElement, result, "");
        return result;
    }
}