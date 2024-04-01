using System.Text.Json;

namespace JJDev.LocalDataService
{
    public class EntityManifestReader
    {
        public string ReadPath { get; private set; }

        public EntityManifestReader(string readPath)
        {
            if (string.IsNullOrWhiteSpace(readPath))
            {
                throw new InvalidOperationException("'readPath' cannot be empty or only whitespace");
            }

            ReadPath = readPath;
        }

        public EntityManifest Read(string ownerId)
        {
            if (string.IsNullOrWhiteSpace (ownerId))
            {
                throw new InvalidOperationException("'ownerId' cannot be empty or only whitespace");
            }

            var entityManifestJson = File.ReadAllText(GetFilePath(ownerId));

            var entityManifest = GetEntityManifest(entityManifestJson);

            return entityManifest ?? new EntityManifest(ReadPath);
        }

        private string GetFilePath(string ownerId)
        {
            return Path.Combine(
                ReadPath,
                $"{ownerId}.json");
        }

        private EntityManifest GetEntityManifest(string entityManifestJson)
        {
            var entityManifest = JsonSerializer.Deserialize<EntityManifest>(
                entityManifestJson,
                new JsonSerializerOptions { IgnoreReadOnlyProperties = true });

            return entityManifest ?? new EntityManifest(ReadPath);
        }
    }
}
