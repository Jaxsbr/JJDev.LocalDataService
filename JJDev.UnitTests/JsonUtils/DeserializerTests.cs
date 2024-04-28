using JJDev.LocalDataService.JsonUtils;
using System.Text.Json;
using System.Reflection;

namespace JJDev.UnitTests.JsonUtils
{
    public class DeserializerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenEmptyOrWhitespace_WhenCallingDeserialize_ThenThrowException(string jsonToDeserialize)
        {
            // act
            var exception = Assert.Throws<ArgumentException>(()
                => Deserializer.Deserialize<object>(jsonToDeserialize));

            // assert
            Assert.NotNull(exception);
            Assert.Equal($"'{nameof(jsonToDeserialize)}' cannot be empty or only whitespace", exception.Message);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenJson_WhenCallingDeserialize_ThenReturnGenericObject(bool propertyNameCaseInsensitive)
        {
            // arrange
            var json = "{\"name\":\"joe\"}";

            // act
            var deserializedObject = Deserializer.Deserialize<object>(
                json, 
                propertyNameCaseInsensitive);

            // assert
            Assert.NotNull(deserializedObject);
            Assert.Equal(json, deserializedObject.ToString());
        }
    }
}
