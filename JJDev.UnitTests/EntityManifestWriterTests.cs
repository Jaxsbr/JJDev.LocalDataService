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
            var entityManifestWriter = new EntityManifestWriter(
                directoryExistsValidatorMock.Object,
                Mock.Of<IFileWriter>(),
                path);

            // assert
            Assert.Equal(path, entityManifestWriter.WritePath);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenConstructing_ThenThrowException(string path)
        {
            // arrange & act
            var exception = Assert.Throws<InvalidOperationException>(()
                => new EntityManifestWriter(
                    Mock.Of<IDirectoryExistsValidator>(),
                    Mock.Of<IFileWriter>(),
                    path));

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
                => new EntityManifestWriter(
                    directoryExistsValidatorMock.Object,
                    Mock.Of<IFileWriter>(),
                    invalidPath));

            // assert
            Assert.NotNull(exception);
            directoryExistsValidatorMock.Verify();
        }

        [Fact]
        public void GivenManifest_WhenCallingWrite_ThenWritesToFile()
        {
            // arrange
            var path = Environment.CurrentDirectory;
            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == path)))
                .Returns(true)
                .Verifiable();

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock
                .Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var entityManifestWriter = new EntityManifestWriter(
                directoryExistsValidatorMock.Object,
                fileWriterMock.Object,
                path);

            var entityManifest = new EntityManifest("ownerId");

            // act
            entityManifestWriter.Write(entityManifest);

            // assert
            fileWriterMock.Verify();
        }
    }
}
