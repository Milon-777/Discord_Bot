using GAIA_Bot;
using GAIA_Bot.StarExpedition.Constants;
using GAIA_Bot.StarExpedition.Repositories;
using GAIA_Bot.StarExpedition.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using OfficeOpenXml;
using System.Diagnostics;

var builder = Host.CreateApplicationBuilder(args);
var token = builder.Configuration["Discord:Token"]
	?? throw new InvalidOperationException("Missing Discord token");


if (string.IsNullOrWhiteSpace(token))
    throw new ArgumentException("Discord token not found.");

builder.Services
	.AddDiscordGateway(options =>
		{
			options.Token = token;
		})
	.AddApplicationCommands()
	.Configure<StarExpeditionOptions>(
	    builder.Configuration.GetSection("StarExpedition"))
	.Configure<IDiscordOptions>(builder.Configuration.GetSection("Discord"))
	.AddSingleton<JsonStorage>()
    .AddSingleton<StarExpeditionRepository>()
	.AddHttpClient<StarExpeditionService>();

var host = builder.Build();
ExcelPackage.License.SetNonCommercialPersonal("Milon Bot");
host.AddModules(typeof(Program).Assembly);

try
{
	await host.RunAsync();
}
catch (Exception ex)
{
	Console.WriteLine(ex);
}
