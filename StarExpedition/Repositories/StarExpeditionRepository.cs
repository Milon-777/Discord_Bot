using GAIA_Bot.StarExpedition.Models;

namespace GAIA_Bot.StarExpedition.Repositories
{
	public sealed class StarExpeditionRepository(JsonStorage jsonStorage)
	{
		internal async Task<Data> LoadAsync(CancellationToken ct = default)
		{
			var json = await File.ReadAllTextAsync(jsonStorage.JsonPath, ct);

			return JsonStorage.Deserialize<Data>(json);
		}

		internal async Task SaveAsync(Data data, CancellationToken ct = default)
		{
			var directory = Path.GetDirectoryName(jsonStorage.JsonPath);

			if (!string.IsNullOrWhiteSpace(directory))
			{
				Directory.CreateDirectory(directory);
			}

			var json = JsonStorage.Serialize(data);

			await File.WriteAllTextAsync(jsonStorage.JsonPath, json, ct);
		}
	}
}
