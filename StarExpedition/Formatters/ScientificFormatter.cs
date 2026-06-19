using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GAIA_Bot.StarExpedition.Formatters
{
	internal static class ScientificFormatter
	{
		internal static string BigNumberFormatter(BigInteger value)
		{
			string s = value.ToString();

			if (s.Length <= 3)
				return s;

			int exponent = s.Length - 1;

			string mantissa = $"{s[0]}.{s.Substring(1, Math.Min(6, s.Length - 1))}";

			return $"{mantissa}E+{exponent}";
		}
	}
}
