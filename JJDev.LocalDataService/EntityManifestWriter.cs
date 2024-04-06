using JJDev.LocalDataService.ExtensionMethods;
using JJDev.LocalDataService.FileIOInterfaces;
using JJDev.LocalDataService.JsonUtils;
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
        /// <exception cref="ArgumentException">Exception is thrown when the writePath argument is empty or only whitespace</exception>
        /// <exception cref="DirectoryNotFoundException">Exception is thrown when writePath is not a valid directory</exception>
        public EntityManifestWriter(
            IDirectoryExistsValidator directoryExistsValidator,
            IFileWriter fileWriter,
            string writePath)
        {
            writePath.ValidateForEmptiness(nameof(writePath));

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
            return Serializer.Serialize(entityManifest);
        }

        private string GetFilePath(EntityManifest entityManifest)
        {
            return Path.Combine(
                WritePath,
                $"{entityManifest.OwnerId}.json");
        }
    }
}
