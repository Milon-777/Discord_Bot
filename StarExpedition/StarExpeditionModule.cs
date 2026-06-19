using GAIA_Bot.StarExpedition.Constants;
using GAIA_Bot.StarExpedition.Models;
using GAIA_Bot.StarExpedition.Parsers;
using GAIA_Bot.StarExpedition.Services;
using Microsoft.Extensions.Logging;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using OfficeOpenXml;
using System.Globalization;
using System.Numerics;
using System.Text.Json;


namespace GAIA_Bot.StarExpedition
{
	public class StarExpeditionModule(StarExpeditionService service, ILogger<StarExpeditionModule> logger) : ApplicationCommandModule<ApplicationCommandContext>
	{
		[SlashCommand("se", "Return Boss HP")]
		public async Task BossHP(
			[SlashCommandParameter(
			Name="boss",
			Description = "Boss stage x",
			MinValue = 1,
			MaxValue = 200
			)] int @boss,

			[SlashCommandParameter(
			Name = "percentage",
			Description = "Percentage of Boss HP",
			MinValue = 1,
			MaxValue = 100
			)] int @percentage = 100)
		{
			await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage());

			try
			{
				var embed = await service.GetBossHpAsync(boss, percentage);

				if (embed is null)
				{
					await Context.Interaction.SendFollowupMessageAsync(
						new InteractionMessageProperties()
							.WithContent("Boss not found.")
					);
					return;
				}

				await Context.Interaction.SendFollowupMessageAsync(
					new InteractionMessageProperties()
						.AddEmbeds(embed)
				);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to get boss HP");

				await Context.Interaction.SendFollowupMessageAsync(
					new InteractionMessageProperties()
						.WithContent("❌ Failed to load boss data.")
				);
			}
		}

		[SlashCommand("se_update", "Update data for Star Expedition from Excel file")]
		public async Task UpdateData(
			[SlashCommandParameter(Name = "excel-file", Description = "Excel file containing updated data for Star Expedition")] Attachment excelFile
			)
		{
			await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage());

			try
			{
				await service.UpdateDataAsync(excelFile);

				await Context.Interaction.SendFollowupMessageAsync(
					new InteractionMessageProperties()
						.WithContent("✅ Updated Star Expedition data.")
				);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Couldn't load the file");

				await Context.Interaction.SendFollowupMessageAsync(
					new InteractionMessageProperties()
						.WithContent($"❌ {ex.Message}")
				);
			}
		}	
	}
}
