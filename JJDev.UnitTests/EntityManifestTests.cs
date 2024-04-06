using JJDev.LocalDataService;

namespace JJDev.UnitTests
{
    public class EntityManifestTests
    {
        [Fact]
        public void GivenOwnerId_WhenConstructing_ThenAssignOwnerId()
        {
            // arrange
            var ownerId = "iamtheboss";

            // act
            var entityManifest = new EntityManifest(ownerId);

            // assert
            Assert.Equal(ownerId, entityManifest.OwnerId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitesapce_WhenConstructing_ThenThrowException(string ownerId)
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentException>(()
                => new EntityManifest(ownerId));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'ownerId' cannot be empty or only whitespace", exception.Message);
        }

        [Fact]
        public void GivenString_WhenCallingAddRecord_ThenReturnsGuid()
        {
            // arrange
            var expectedRecordCount = 1;
            var entityType = "someobject";
            var ownerId = "iamtheboss";
            var entityManifest = new EntityManifest(ownerId);

            // act
            var recordId = entityManifest.AddRecord(entityType);

            // assert
            var guid = Assert.IsAssignableFrom<Guid>(recordId);
            Assert.NotEqual(Guid.Empty, guid);
            Assert.Equal(expectedRecordCount, entityManifest.RecordCount);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenCallingAddRecord_ThenThrowException(string entityType)
        {
            // arrange
            var expectedRecordCount = 0;
            var ownerId = "iamtheboss";
            var entityManifest = new EntityManifest(ownerId);

            // act
            var exception = Assert.Throws<ArgumentException>(()
                => entityManifest.AddRecord(entityType));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'entityType' cannot be empty or only whitespace", exception.Message);
            Assert.Equal(expectedRecordCount, entityManifest.RecordCount);
        }
    }
}