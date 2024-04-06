using JJDev.LocalDataService.JsonUtils;
using System.Text.Json;

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
            var person = new { Name = "John Doe", Age = 30 };
            var personJson = JsonSerializer.Serialize(person);

            // act
            var deserializedObject = Deserializer.Deserialize<object>(
                personJson, 
                propertyNameCaseInsensitive);

            // assert
             Assert.NotNull(deserializedObject);
            Assert.Equal(personJson, deserializedObject.ToString());
        }
    }
}
