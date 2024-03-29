using JJDev.LocalDataService;

namespace JJDev.UnitTests
{
    public class EntityManifestTests
    {
        [Fact]
        public void GiveOwnerId_WhenConstructing_ThenAssignOwnerId()
        {
            // arrange
            var ownerId = "iamtheboss";

            // act
            var entityManifest = new EntityManifest(ownerId);

            // assert
            Assert.Equal(ownerId, entityManifest.OwnerId);
        }
    }
}