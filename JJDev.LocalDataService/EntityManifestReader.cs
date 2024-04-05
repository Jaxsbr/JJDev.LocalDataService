using JJDev.LocalDataService.FileIOInterfaces;
using System.Text.Json;

namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifestReader takes a file path and reads it's content to an EntityManifest
    /// </summary>
    public class EntityManifestReader
    {
        private readonly IFileReader _fileReader;

        public string ReadPath { get; private set; }

        /// <summary>
        /// EntityManifestReader constructor
        /// </summary>
        /// <param name="directoryExistsValidator">An implementation of the IDirectoryExistsValidator interface.
        /// The class that performs directory existance validation</param>
        /// <param name="fileReader">An implementation of the IFileReader interface. The class that performs the JSON retrieval</param>
        /// <param name="readPath">A directory where an EntityManifestReader will attempt to read from</param>
        /// <exception cref="InvalidOperationException">Exception is thrown when the readPath argument is empty or only whitespace</exception>
        /// <exception cref="DirectoryNotFoundException">Exception is thrown when readPath is not a valid directory</exception>
        public EntityManifestReader(
            IDirectoryExistsValidator directoryExistsValidator,
            IFileReader fileReader,
            string readPath)
        {
            if (string.IsNullOrWhiteSpace(readPath))
            {
                throw new InvalidOperationException("'readPath' cannot be empty or only whitespace");
            }

            if(!directoryExistsValidator.Validate(readPath))
            {
                throw new DirectoryNotFoundException();
            }

            _fileReader = fileReader;
            ReadPath = readPath;
        }

        public EntityManifest Read(string ownerId)
        {
            if (string.IsNullOrWhiteSpace (ownerId))
            {
                throw new InvalidOperationException("'ownerId' cannot be empty or only whitespace");
            }

            var entityManifestJson = _fileReader.Read(GetFilePath(ownerId));

            if (string.IsNullOrWhiteSpace(entityManifestJson))
            {
                throw new IOException("EntityManifest failed to load. 'filePath' did not contain valid JSON");
            }

            return GetEntityManifest(entityManifestJson);
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
