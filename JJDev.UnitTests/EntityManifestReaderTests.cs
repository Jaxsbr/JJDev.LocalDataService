using JJDev.LocalDataService;
using JJDev.LocalDataService.Utils;
using Moq;

namespace JJDev.UnitTests
{
    public class EntityManifestReaderTests
    {
        [Fact]
        public void GivenReadPath_WhenConstructing_ThenAssignReadPath()
        {
            // arrange
            var readPath = Environment.CurrentDirectory;

            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == readPath)))
                .Returns(true)
                .Verifiable();

            // act
            var entityManifestReader = new EntityManifestReader(
                directoryExistsValidatorMock.Object,
                Mock.Of<IFileReader>(),
                readPath);

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
                => new EntityManifestReader(
                    Mock.Of<IDirectoryExistsValidator>(),
                    Mock.Of<IFileReader>(),
                    readPath));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("'readPath' cannot be empty or only whitespace", exception.Message);
        }

        [Theory]
        [InlineData("./xx")]
        [InlineData("abc")]
        public void GivenInvalidReadPath_WhenConstructing_ThenThrowException(string readPath)
        {
            // arrange
            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == readPath)))
                .Returns(false)
                .Verifiable();

            // act
            var exception = Assert.Throws<DirectoryNotFoundException>(()
                => new EntityManifestReader(
                    directoryExistsValidatorMock.Object,
                    Mock.Of<IFileReader>(),
                    readPath));

            // assert
            Assert.NotNull(exception);
            directoryExistsValidatorMock.Verify();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenCallingRead_ThenThrowException(string ownerId)
        {
            // arrange
            var readPath = Environment.CurrentDirectory;

            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.IsAny<string>()))
                .Returns(true);

            var entityManifestReader = new EntityManifestReader(
                directoryExistsValidatorMock.Object,
                Mock.Of<IFileReader>(),
                readPath);

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
            var filePath = Path.Combine(readPath, $"{ownerId}.json");
            var entityManifestJsonMock = $"\"OwnerId\":\"{ownerId}\"";
            entityManifestJsonMock = "{" + entityManifestJsonMock + "}";

            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.Read(It.Is<string>(x => x == filePath)))
                .Returns(entityManifestJsonMock)
                .Verifiable();

            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == readPath)))
                .Returns(true)
                .Verifiable();

            var entityManifestReader = new EntityManifestReader(
                directoryExistsValidatorMock.Object,
                fileReaderMock.Object,
                readPath);

            // act
            var entityManifest = entityManifestReader.Read(ownerId);

            // assert
            Assert.NotNull(entityManifest);
            Assert.Equal(ownerId, entityManifest.OwnerId);
            fileReaderMock.Verify();
            directoryExistsValidatorMock.Verify();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespaceJsonInFile_WhenCallingRead_ThenThrowException(string entityManifestJson)
        {
            // arrange
            var ownerId = "iamtheboss";
            var readPath = Environment.CurrentDirectory;
            var filePath = Path.Combine(readPath, $"{ownerId}.json");

            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.Read(It.IsAny<string>()))
                .Returns(entityManifestJson)
                .Verifiable();

            var directoryExistsValidatorMock = new Mock<IDirectoryExistsValidator>();
            directoryExistsValidatorMock
                .Setup(x => x.Validate(It.Is<string>(y => y == readPath)))
                .Returns(true)
                .Verifiable();

            var entityManifestReader = new EntityManifestReader(
                directoryExistsValidatorMock.Object,
                fileReaderMock.Object,
                readPath);

            // act
            var exception = Assert.Throws<IOException>(()
                => entityManifestReader.Read(ownerId));

            // assert
            Assert.NotNull(exception);
            Assert.Equal("EntityManifest failed to load. 'filePath' did not contain valid JSON", exception.Message);
            fileReaderMock.Verify();
            directoryExistsValidatorMock.Verify();
        }

        // TODO:
        // - Refactor serialization logic
        // - Refactor reused constructor mock logic in tests
        // - Research attribute for empty/whitespace string validation
    }
}
