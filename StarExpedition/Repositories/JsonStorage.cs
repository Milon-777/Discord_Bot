using GAIA_Bot.StarExpedition.Constants;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GAIA_Bot.StarExpedition.Repositories
{
	public sealed class JsonStorage
	{
		internal string JsonPath { get; }

		public JsonStorage()
		{
			var dir = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"GAIA_Bot");

			if (!string.IsNullOrWhiteSpace(dir))
			{
				Directory.CreateDirectory(dir);
			}

			JsonPath = Path.Combine(dir, "star_expedition.json") ?? throw new InvalidOperationException("StarExpedition:JsonPath is not configured.");
		}

		private static readonly JsonSerializerOptions s_writeOptions = new()
		{
			WriteIndented = true
		};

		private static readonly JsonSerializerOptions s_readOptions = new()
		{
			AllowTrailingCommas = true,
			PropertyNameCaseInsensitive = true
		};

		internal static string Serialize<T>(T value)
		{
			return JsonSerializer.Serialize(value, s_writeOptions);
		}

		internal static T Deserialize<T>(string json)
		{
			return JsonSerializer.Deserialize<T>(json, s_readOptions)
				?? throw new JsonException("Failed to deserialize JSON.");
		}
	}
}
