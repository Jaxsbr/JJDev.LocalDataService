namespace JJDev.LocalDataService
{
    public struct EntityManifestRecord
    {
        public string Id { get; private set; }

        public string EntityType { get; private set; }

        public EntityManifestRecord(string id, string entityType)
        {
            Id = id;
            EntityType = entityType;
        }
    }
}
