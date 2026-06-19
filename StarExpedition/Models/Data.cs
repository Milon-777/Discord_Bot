using System;
using System.Collections.Generic;
using System.Text;

namespace GAIA_Bot.StarExpedition.Models
{
	internal class Data
	{
		public string LastUpdated { get; set; } = string.Empty;
		public List<BossDataDto> Bosses { get; set; } = [];
	}
}
