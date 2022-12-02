using System.Globalization;
using System.Text.RegularExpressions;

namespace GdscManagement.Utilities.Extensions;

public static partial class StringExtensions
{
    /**
     * <summary>
     * See more at https://stackoverflow.com/a/5796793/10822009
     * </summary>
     */
    public static string SplitCamelCase(this string str)
    {
        var notLowercase = HandleNotLowercase().Replace(str, "$1 $2");
        return HandleLowercase().Replace(notLowercase, "$1 $2");
    }

    public static string FirstToUpper(this string str)
    {
        return str.First().ToString().ToUpper() + str[1..].ToLower();
    }

    [GeneratedRegex("(\\p{Ll})(\\P{Ll})")]
    private static partial Regex HandleLowercase();

    [GeneratedRegex("(\\P{Ll})(\\P{Ll}\\p{Ll})")]
    private static partial Regex HandleNotLowercase();
}
