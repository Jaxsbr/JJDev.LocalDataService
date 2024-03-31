using JJDev.LocalDataService.Utils;

namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifestWriter takes and entity manifest and writes it to a file
    /// </summary>
    public class EntityManifestWriter
    {
        private readonly IDirectoryExistsValidator _directoryExistsValidator;

        public string Path { get; private set; }

        public EntityManifestWriter(IDirectoryExistsValidator directoryExistsValidator, string path)
        {
            _directoryExistsValidator = directoryExistsValidator;

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("'path' cannot be empty or only whitespace");
            }

            if (!_directoryExistsValidator.Validate(path))
            {
                throw new DirectoryNotFoundException();
            }

            Path = path;
        }
    }
}
