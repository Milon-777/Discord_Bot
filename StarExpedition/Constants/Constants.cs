using System;
using System.Collections.Generic;
using System.Text;

namespace GAIA_Bot.StarExpedition.Constants
{
	internal static class ImageLinks
	{
		internal static string BossThumbnailUrl {get; } = "https://cdn.discordapp.com/attachments/1283076276973862952/1517078717384626277/ChatGPT_Image_Jun_18_2026_11_08_31_AM.png";
	}

	public sealed class StarExpeditionOptions
	{
		public string JsonPath { get; init; } = default!;
	}
}
