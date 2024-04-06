using JJDev.LocalDataService.ExtensionMethods;
using System.Text.Json;

namespace JJDev.LocalDataService.JsonUtils
{
    public static class Deserializer
    {
        public static TObject Deserialize<TObject>(string jsonToDeserialize, bool propertyNameCaseInsensitive = true)
            where TObject : new()
        {
            jsonToDeserialize.ValidateForEmptiness(nameof(jsonToDeserialize));

            var deserializedObject = JsonSerializer.Deserialize<TObject>(
                jsonToDeserialize,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = propertyNameCaseInsensitive });

            return deserializedObject ?? new TObject();
        }
    }
}
