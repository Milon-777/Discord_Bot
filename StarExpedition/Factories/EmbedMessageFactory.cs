using GAIA_Bot.StarExpedition.Constants;
using GAIA_Bot.StarExpedition.Formatters;
using NetCord;
using NetCord.Rest;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GAIA_Bot.StarExpedition.Factories
{
	internal static class EmbedMessageFactory
	{
		internal static EmbedProperties BuildEmbed(
			int boss,
			int percentage,
			BigInteger remainingHp,
			string lastUpdated)
		{
			return new EmbedProperties()
				.WithTitle("Star Expedition")
				.WithDescription($"Boss x{boss} at {percentage}% HP")
				.WithColor(new Color(0xD83E8F))
				.WithThumbnail(new EmbedThumbnailProperties(ImageLinks.BossThumbnailUrl))
				.WithFooter(
					new EmbedFooterProperties()
						.WithText($"Data was updated on {lastUpdated}")
				)
				.AddFields(
					new EmbedFieldProperties()
						.WithName("❤️ Remaining HP")
						.WithValue(ScientificFormatter.BigNumberFormatter(remainingHp))
						.WithInline(true)
				);
		}
	}
}
