using JJDev.LocalDataService.JsonUtils;

namespace JJDev.UnitTests.JsonUtils
{
    public class SerializerTests
    {
        [Fact]
        public void GivenNull_WhenCallingSerialize_ThenThrowException()
        {
            // arrange
            object? obj = null;

            // act
            var exception = Assert.Throws<ArgumentNullException>(()
                => Serializer.Serialize(obj));

            // assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void GivenObject_WhenCallingSerialize_ThenReturnJson()
        {
            // arrange
            var expected = "{\"Name\":\"John Doe\",\"Age\":30}";
            var person = new { Name = "John Doe", Age = 30 };

            // act
            var result = Serializer.Serialize(person);

            // assert
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
