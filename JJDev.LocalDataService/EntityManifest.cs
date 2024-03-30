namespace JJDev.LocalDataService
{
    /// <summary>
    /// EntityManifest represents a physical file on the local computer.
    /// The file contains a record of the owner and a collection of entity records for
    /// each entity currently stored on the local computer.
    /// </summary>
    public class EntityManifest
    {
        public int RecordCount {  get { return _records.Count; } }

        public string OwnerId { get; private set; }

        private List<EntityManifestRecord> _records;

        public EntityManifest(string ownerId)
        {
            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new InvalidOperationException("'ownerId' cannot be empty or only whitespace");
            }

            OwnerId = ownerId;
            _records = new List<EntityManifestRecord>();
        }

        /// <summary>
        /// AddRecord creates a new entity record in the manifest
        /// </summary>
        /// <param name="entityType">Provide the name of the class object represented by the new entity record</param>
        /// <returns>Returns a Guid record id for the newly created entity record</returns>
        public Guid AddRecord(string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new InvalidOperationException("'entityType' cannot be empty or only whitespace");
            }

            var recordId = Guid.NewGuid();

            _records.Add(
                new EntityManifestRecord
                {
                    Id = recordId.ToString(),
                    EntityType = entityType
                });

            return recordId;
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
