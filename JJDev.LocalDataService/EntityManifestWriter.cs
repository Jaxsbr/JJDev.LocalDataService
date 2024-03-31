using JJDev.LocalDataService.Utils;
using System.Text.Json;

namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifestWriter takes and entity manifest and writes it to a file
    /// </summary>
    public class EntityManifestWriter
    {
        private readonly IDirectoryExistsValidator _directoryExistsValidator;

        public string WritePath { get; private set; }

        public EntityManifestWriter(IDirectoryExistsValidator directoryExistsValidator, string writePath)
        {
            if (string.IsNullOrWhiteSpace(writePath))
            {
                throw new InvalidOperationException("'path' cannot be empty or only whitespace");
            }

            _directoryExistsValidator = directoryExistsValidator;

            if (!_directoryExistsValidator.Validate(writePath))
            {
                throw new DirectoryNotFoundException();
            }

            WritePath = writePath;
        }

        public void Write(EntityManifest entityManifest)
        {
            // Serialize to JSON
            var entityManifestJsonContent = "";
            JsonSerializer.Serialize(
                entityManifest,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Build file name from manifest owner Id and write path
            var fileName = $"{entityManifest.OwnerId}.json";
            var filePath = Path.Combine(WritePath, fileName);

            // Writer JSON content to file path
            File.WriteAllText(filePath, entityManifestJsonContent);
        }
    }
}
