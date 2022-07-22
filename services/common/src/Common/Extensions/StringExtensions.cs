namespace Commons.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string value)
        {
            Validate(value);
            const string Underscore = "_";
            return string.Concat(
                value.Select(
                    (x, i) => i > 0 && char.IsUpper(x) ? Underscore + x
                        : x.ToString())).ToLower();
        }

        public static string AddQuotes(this string value)
        {
            Validate(value);
            return $"\"{value}\"";
        }

        public static string RemoveQuotes(this string value)
        {
            Validate(value);
            return value.Trim('"');
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} cannot be null or empty");
            }
        }
    }
}
