using System.Globalization;
using System.Numerics;

namespace GAIA_Bot.StarExpedition.Parsers
{
	internal static class BigIntegerParser
	{
		internal static BigInteger ParseBigIntegerScientific(string raw)
		{
			raw = raw.Trim();

			raw = raw.Replace(" ", "").Replace("\u00A0", "");

			raw = raw.Replace(",", ".");

			if (!raw.Contains('E') && !raw.Contains('e'))
			{
				raw = raw.Replace(".", "");
				return BigInteger.Parse(raw);
			}

			int eIndex = raw.IndexOfAny(['E', 'e']);
			if (eIndex < 0)
				throw new FormatException($"Invalid scientific notation: {raw}");

			string mantissa = raw[..eIndex];
			string exponentString = raw[(eIndex + 1)..];
			if (string.IsNullOrWhiteSpace(exponentString))
				throw new FormatException($"Invalid scientific notation: {raw}");

			int exponent = int.Parse(exponentString, CultureInfo.InvariantCulture);

			int decimalIndex = mantissa.IndexOf('.');

			int decimals = 0;

			if (decimalIndex >= 0)
			{
				decimals = mantissa.Length - decimalIndex - 1;
				mantissa = mantissa.Replace(".", "");
			}

			BigInteger result = BigInteger.Parse(mantissa);

			exponent -= decimals;

			if (exponent > 0)
				result *= BigInteger.Pow(10, exponent);

			return result;
		}
	}
}
