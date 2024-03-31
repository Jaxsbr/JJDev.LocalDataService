namespace JJDev.LocalDataService
{
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
