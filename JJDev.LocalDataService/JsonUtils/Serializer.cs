using System.Text.Json;

namespace JJDev.LocalDataService.JsonUtils
{
    public static class Serializer
    {
        public static string Serialize<TObject>(TObject objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException(nameof(objectToSerialize));
            }

            return JsonSerializer.Serialize(objectToSerialize);
        }
    }
}
