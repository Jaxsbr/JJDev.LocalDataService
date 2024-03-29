namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifest represents a physical file on the local computer.
    /// The file contains a record of the owner and a collection of entity records for
    /// each entity currently stored on the local computer.
    /// </summary>
    public class EntityManifest
    {
        public string OwnerId { get; private set; }

        private List<EntityManifestRecord> _records;

        public EntityManifest(string ownerId)
        {
            OwnerId = ownerId;
            _records = new List<EntityManifestRecord>();
        }
    }

    internal struct EntityManifestRecord
    {
        internal string Id { get; set; }

        internal string EntityType { get; set; }

        internal EntityManifestRecord(string id, string entityType)
        {
            Id = id;
            EntityType = entityType;
        }
    }


}
