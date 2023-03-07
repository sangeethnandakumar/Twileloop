namespace Twileloop.Helpers {
    public static class Extensions {
        public static void AddKeywords(this List<string> list, List<string> baseList, params string[] keywords) {
            var newArray = keywords.ToList();
            newArray.AddRange(baseList);
            list = newArray;
        }
    }
}
