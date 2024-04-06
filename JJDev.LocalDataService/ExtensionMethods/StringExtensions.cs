namespace JJDev.LocalDataService.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool ValidateForEmptiness(
            this string stringToValidate,
            string nameOfStringArgument = "",
            bool throwExceptionOnEmpty = true)
        {
            if (!string.IsNullOrWhiteSpace(stringToValidate))
            {
                return true;
            }

            if (!throwExceptionOnEmpty) 
            {
                return false;
            }

            string argumentName = nameof(stringToValidate);
            if (!string.IsNullOrWhiteSpace(nameOfStringArgument))
            {
                argumentName = nameOfStringArgument;
            }

            var exceptionMessage =
                $"'{argumentName}' cannot be empty or only whitespace";

            throw new ArgumentException(exceptionMessage);
        }
    }
}
