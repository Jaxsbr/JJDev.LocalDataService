using JJDev.LocalDataService;

namespace JJDev.UnitTests
{
    public class EntityManifestReaderTests
    {
        [Fact]
        public void GivenReadPath_WhenConstructing_ThenAssignReadPath()
        {
            // arrange
            var readPath = Environment.CurrentDirectory;

            // act
            var entityManifestReader = new EntityManifestReader(readPath);

            // assert
            Assert.Equal(readPath, entityManifestReader.ReadPath);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenConstructing_ThenThrowException(string readPath)
        {
            // act
            var exception = Assert.Throws<InvalidOperationException>(()
                => new EntityManifestReader(readPath));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'readPath' cannot be empty or only whitespace", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenCallingRead_ThenThrowException(string ownerId)
        {
            // arrange
            var readPath = Environment.CurrentDirectory;
            var entityManifestReader = new EntityManifestReader(readPath);

            // act
            var exception = Assert.Throws<InvalidOperationException>(()
                => entityManifestReader.Read(ownerId));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'ownerId' cannot be empty or only whitespace", exception.Message);
        }

        [Fact]
        public void GivenOwnerId_WhenCallingRead_ThenReturnsManifest()
        {
            // arrange
            var ownerId = "iamtheboss";
            var readPath = Environment.CurrentDirectory;
            var entityManifestReader = new EntityManifestReader(readPath);

            // act
            var entityManifest = entityManifestReader.Read(ownerId);

            // assert
            Assert.NotNull(entityManifest);
            Assert.Equal(ownerId, entityManifest.OwnerId);
        }
    }
}
