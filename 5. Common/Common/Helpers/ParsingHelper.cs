using Common.Exceptions;
using System.Globalization;

namespace Common.Helpers
{
    public static class ParsingHelper
    {
        public static int ParseInt(this string value)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }
    }
}
