namespace JJDev.LocalDataService
{
    public class EntityManifest
    {
        public string OwnerId { get; private set; }

        public EntityManifest(string ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
