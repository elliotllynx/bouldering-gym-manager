using System.Text.RegularExpressions;

namespace BoulderSetManager.Models.Helpers
{
    public static class GradeHelper
    {
        private static readonly Regex GradeRegex = new(@"^([4-9]|10)([A-C])(\+?)$");

        public static bool IsValidGrade(string grade)
        {
            if (string.IsNullOrWhiteSpace(grade)) return false;
            return GradeRegex.IsMatch(grade);
        }

        public static int ParseGrade(string grade)
        {
            if (string.IsNullOrWhiteSpace(grade)) return 0;
            var match = GradeRegex.Match(grade);
            if (!match.Success) return 0;

            int number = int.Parse(match.Groups[1].Value);
            int letter = match.Groups[2].Value[0] - 'A';
            int plus = match.Groups[3].Value == "+" ? 1 : 0;

            return (number - 4) * 6 + letter * 2 + plus; // numeric score
        }

        public static string FormatGrade(int score)
        {
            int number = score / 6 + 4;
            int remainder = score % 6;
            char letter = (char)('A' + remainder / 2);
            string plus = remainder % 2 == 1 ? "+" : "";
            return $"{number}{letter}{plus}";
        }
    }
}
