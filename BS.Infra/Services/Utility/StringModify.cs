using System.Text.RegularExpressions;

namespace BS.Infra.Services.Utility
{
    public static class StringModify
    {
        public static string ReplaceExact(string input, string oldValue, string newValue)
        {
            var pattern = $@"\b{Regex.Escape(oldValue)}\b";
            var result = Regex.Replace(input, pattern, newValue, RegexOptions.IgnoreCase);

            return result;
        }
    }
}
