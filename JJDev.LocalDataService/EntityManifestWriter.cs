namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifestWriter takes and entity manifest and writes it to a file
    /// </summary>
    public class EntityManifestWriter
    {
        public string Path { get; private set; }

        public EntityManifestWriter(string path) 
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("'path' cannot be empty or only whitespace");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            Path = path;
        }
    }
}
