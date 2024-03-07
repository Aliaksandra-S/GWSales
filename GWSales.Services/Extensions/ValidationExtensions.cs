using System.Text.RegularExpressions;

namespace GWSales.Services.Extensions;

public static class ValidationExtensions
{
    public static bool IsValidClotherSize(this string input)
    {
        // 42 or 40(186) is valid
        // 1 or 40(1) or 4(174) is not valid

        var regex = new Regex(@"^\d{2}(?:\(\d{3}\))?$");

        if (!regex.IsMatch(input))
        {
            return false;
        }

        if (input.TryExtractValues(out int first, out int second))
        {
            if (first < 38 && first > 56)
            {
                return false;
            }

            if (second < 150 && second > 200)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    private static bool TryExtractValues(this string input, out int first, out int second)
    {
        first = 0;
        second = 0;
        var parts = input.Split('(', ')');

        if (parts.Length == 1)
        {
            if (int.TryParse(parts[0], out first))
            {
                return true;
            }
        }

        if (parts.Length == 2)
        {
            if (int.TryParse(parts[0], out first) && int.TryParse(parts[1], out second))
            {
                return true;
            }
        }

        return false;
    }
}
