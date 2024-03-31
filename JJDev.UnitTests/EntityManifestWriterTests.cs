using JJDev.LocalDataService;
using JJDev.LocalDataService.Utils;
using Moq;

namespace JJDev.UnitTests
{
    public class EntityManifestWriterTests
    {
        [Fact]
        public void GivenPath_WhenConstructing_ThenAssignPath()
        {
            // arrange
            var path = Environment.CurrentDirectory;
            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == path)))
                .Returns(true)
                .Verifiable();

            // act
            var entityManifestWriter = new EntityManifestWriter(directoryExistsValidatorMock.Object, path);

            // assert
            Assert.Equal(path, entityManifestWriter.Path);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenConstructing_ThenThrowException(string path)
        {
            // arrange & act
            var exception = Assert.Throws<InvalidOperationException>(() 
                => new EntityManifestWriter(Mock.Of<IDirectoryExistsValidator>(), path));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'path' cannot be empty or only whitespace", exception.Message);
        }

        [Fact]
        public void GivenInvalidPath_WhenConstructing_ThenThrowException()
        {
            // arrange
            var invalidPath = "xxx";
            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == invalidPath)))
                .Returns(false)
                .Verifiable();

            // act
            var exception = Assert.Throws<DirectoryNotFoundException>(() 
                => new EntityManifestWriter(directoryExistsValidatorMock.Object, invalidPath));

            // assert
            Assert.NotNull(exception);
            directoryExistsValidatorMock.Verify();
        }
    }
}
