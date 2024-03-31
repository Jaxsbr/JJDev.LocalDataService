using JJDev.LocalDataService;

namespace JJDev.UnitTests
{
    public class EntityManifestWriterTests
    {
        [Fact]
        public void GivenPath_WhenConstructing_ThenAssignPath()
        {
            // arrange
            var path = Environment.CurrentDirectory;

            // act
            var entityManifestWriter = new EntityManifestWriter(path);

            // assert
            Assert.Equal(path, entityManifestWriter.Path);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenConstructing_ThenThrowException(string path)
        {
            // arrange & act
            var exception = Assert.Throws<InvalidOperationException>(() => new EntityManifestWriter(path));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'path' cannot be empty or only whitespace", exception.Message);
        }

        [Fact]
        public void GivenInvalidPath_WhenConstructing_ThenThrowException()
        {
            // arrange
            var invalidPath = "./xxx";

            // act
            var exception = Assert.Throws<DirectoryNotFoundException>(() =>
                new EntityManifestWriter(invalidPath));

            // assert
            Assert.NotNull(exception);
        }
    }
}
