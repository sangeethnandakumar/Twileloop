namespace Packages.Twileloop.Helpers {
    public static class Extensions {

        private static Random rng = new Random();

        public static void AddKeywords(this List<string> list, List<string> baseList, params string[] keywords) {
            var newArray = keywords.ToList();
            newArray.AddRange(baseList);
            list = newArray;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(item => rng.Next());
        }

        public static string TrimWithEllipse(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length <= maxLength)
                return input;

            return input.Substring(0, maxLength) + "...";
        }
    }
}
