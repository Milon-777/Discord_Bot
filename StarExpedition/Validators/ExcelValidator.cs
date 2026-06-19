using NetCord;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAIA_Bot.StarExpedition.Validators
{
	internal static class ExcelValidator
	{
		internal static void ValidateExcelFile(Attachment excelFile)
		{
			if (!excelFile.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
				excelFile.ContentType != "application/vnd.ms-excel")
			{
				throw new ArgumentException("Invalid file type. Please upload a valid Excel (.xlsx) file.");
			}
		}
	}
}
