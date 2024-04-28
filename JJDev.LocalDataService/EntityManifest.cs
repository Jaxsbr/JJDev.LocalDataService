using JJDev.LocalDataService.ExtensionMethods;
using System.Collections.ObjectModel;

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

        public ReadOnlyCollection<EntityManifestRecord> Records { get; private set; }

        public EntityManifest(string ownerId)
        {
            ownerId.ValidateForEmptiness(nameof(ownerId));

            OwnerId = ownerId;

            _records = new List<EntityManifestRecord>();

            Records = new ReadOnlyCollection<EntityManifestRecord>(_records);
        }

        /// <summary>
        /// AddRecord creates a new entity record in the manifest
        /// </summary>
        /// <param name="entityType">Provide the name of the class object represented by the new entity record</param>
        /// <returns>Returns a Guid record id for the newly created entity record</returns>
        public Guid AddRecord(string entityType)
        {
            entityType.ValidateForEmptiness(nameof(entityType));

            var recordId = Guid.NewGuid();

            _records.Add(
                new EntityManifestRecord(
                    recordId.ToString(),
                    entityType
                ));

            return recordId;
        }
    }
}
