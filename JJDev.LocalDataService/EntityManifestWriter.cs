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
        private readonly IFileWriter _fileWriter;

        public string WritePath { get; private set; }

        public EntityManifestWriter(
            IDirectoryExistsValidator directoryExistsValidator,
            IFileWriter fileWriter,
            string writePath)
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

            _fileWriter = fileWriter;
            WritePath = writePath;
        }

        public void Write(EntityManifest entityManifest)
        {
            _fileWriter.Write(
                GetFilePath(entityManifest),
                GetEntityManifestJson(entityManifest));
        }

        private string GetEntityManifestJson(EntityManifest entityManifest)
        {
            return JsonSerializer.Serialize(
                entityManifest,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private string GetFilePath(EntityManifest entityManifest)
        {
            return Path.Combine(
                WritePath,
                $"{entityManifest.OwnerId}.json");
        }
    }
}
