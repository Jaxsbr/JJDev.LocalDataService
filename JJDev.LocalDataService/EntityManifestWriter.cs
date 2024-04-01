using JJDev.LocalDataService.Utils;
using System.Text.Json;

namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifestWriter takes an EntityManifest and writes it to a file
    /// </summary>
    public class EntityManifestWriter
    {
        private readonly IFileWriter _fileWriter;

        public string WritePath { get; private set; }

        /// <summary>
        /// EntityManifestWriter
        /// </summary>
        /// <param name="directoryExistsValidator">An implementation of the IDirectoryExistsValidator interface.
        /// The class that performs directory existance validation</param>
        /// <param name="fileWriter">An implementation fo the IFileWriter interface. The class that performs text/JSON file writing</param>
        /// <param name="writePath">A directory where the fileWriter will output to</param>
        /// <exception cref="InvalidOperationException">Exception is thrown when the writePath argument is empty or only whitespace</exception>
        /// <exception cref="DirectoryNotFoundException">Exception is thrown when writePath is not a valid directory</exception>
        public EntityManifestWriter(
            IDirectoryExistsValidator directoryExistsValidator,
            IFileWriter fileWriter,
            string writePath)
        {
            if (string.IsNullOrWhiteSpace(writePath))
            {
                throw new InvalidOperationException("'path' cannot be empty or only whitespace");
            }

            if (!directoryExistsValidator.Validate(writePath))
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
