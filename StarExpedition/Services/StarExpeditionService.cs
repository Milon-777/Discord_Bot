using GAIA_Bot.StarExpedition.Constants;
using GAIA_Bot.StarExpedition.Factories;
using GAIA_Bot.StarExpedition.Formatters;
using GAIA_Bot.StarExpedition.Models;
using GAIA_Bot.StarExpedition.Parsers;
using GAIA_Bot.StarExpedition.Repositories;
using GAIA_Bot.StarExpedition.Validators;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway.JsonModels;
using NetCord.Rest;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace GAIA_Bot.StarExpedition.Services
{
	public sealed class StarExpeditionService(HttpClient httpClient, StarExpeditionRepository repository)
	{	
		public async Task<EmbedProperties?> GetBossHpAsync(int boss, int percentage, CancellationToken ct = default)
		{
			var data = await repository.LoadAsync(ct);

			var bossData = data.Bosses.FirstOrDefault(x => x.Level == boss);

			if (bossData is null)
				return null;

			var hp = BigInteger.Parse(bossData.HP);
			var remainingHp = hp * percentage / 100;

			return EmbedMessageFactory.BuildEmbed(boss, percentage, remainingHp, data.LastUpdated);
		}

		public async Task UpdateDataAsync(Attachment excelFile, CancellationToken ct = default)
		{
			ExcelValidator.ValidateExcelFile(excelFile);

			var stream = await httpClient.GetStreamAsync(excelFile.Url, ct);

			using var package = new ExcelPackage(stream);
			var sheet = package.Workbook.Worksheets[0];

			var bosses = ExcelParser.ParseExcel(sheet);

			var database = new Data
			{
				LastUpdated = DateTime.Now.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
				Bosses = bosses
			};

			await repository.SaveAsync(database, ct);
		}

	}
}
