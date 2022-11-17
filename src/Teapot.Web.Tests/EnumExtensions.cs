namespace Teapot.Web.Tests;

public static class EnumExtensions
{
    public static bool IsOneOf(this Enum enumeration, params Enum[] enums) => enums.Contains(enumeration);
}