using GAIA_Bot.StarExpedition.Models;
using OfficeOpenXml;
using System.ComponentModel;
using System.Globalization;

namespace GAIA_Bot.StarExpedition.Parsers
{
	internal static class ExcelParser
	{
		internal static List<BossDataDto> ParseExcel(ExcelWorksheet sheet)
		{
			ExcelPackage.License.SetNonCommercialPersonal("Milon Bot");
			var bosses = new List<BossDataDto>();
			const int LEVEL_COLUMN = 1;
			const int HP_COLUMN = 2;
			const int START_ROW = 2;
			int current_row = START_ROW;

			while (sheet.Cells[current_row, LEVEL_COLUMN].Value != null)
			{
				var raw_hp = sheet.Cells[current_row, HP_COLUMN].Value?.ToString()?.Trim();

				if (string.IsNullOrWhiteSpace(raw_hp))
					throw new ArgumentNullException($"HP missing at row {current_row}");

				var hp = BigIntegerParser.ParseBigIntegerScientific(raw_hp);

				bosses.Add(new BossDataDto
				{
					Level = int.Parse(sheet.Cells[current_row, LEVEL_COLUMN].Text, CultureInfo.InvariantCulture),
					HP = hp.ToString()
				});

				current_row++;
			}

			return bosses;
		}
	}
}
