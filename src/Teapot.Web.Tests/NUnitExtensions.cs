using NUnit.Framework.Constraints;
using System.Text.RegularExpressions;

namespace Teapot.Web.Tests
{
    public static class NUnitExtensions
    {
        private static readonly Regex _whitespaceRegex = new(@"\s+");

        private static string ReplaceSpaces(string s) => _whitespaceRegex.Replace(s, "");

        public static EqualConstraint IgnoreWhitespace(this EqualConstraint constraint)
            => constraint.Using<string>((actual, expected) => ReplaceSpaces(actual) == ReplaceSpaces(expected));
    }
}