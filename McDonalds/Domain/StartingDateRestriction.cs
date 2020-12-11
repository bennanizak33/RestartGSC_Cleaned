using McDonalds.commun.Constants;
using McDonalds.Commun;
using McDonalds.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
	public class StartingDateRestriction
	{
		private static bool Holidays(McDonaldsContext context, DateTime currentDate)
		{
			DateTime dateDebut = currentDate.Date.AddDays(-1);

			DateTime dateFin = currentDate.Date.AddDays(1);

			return context
				.Holydays
				.AsNoTracking()
				.Any(h => h.Date < dateDebut && h.Date > dateFin );
		}

		private static bool WeekDates(McDonaldsContext context, DateTime currentDate)
		{
			string[] result = AppSettings.ReadSetting(AppSettingConstants.AuthorizedWeekDate, string.Empty).Split(',');

			return result.Any(r => result.Contains(currentDate.ToString("dddd"), StringComparer.OrdinalIgnoreCase));
		}

		public static bool IsValid(McDonaldsContext context, DateTime currentDate)
		{
			return WeekDates(context, currentDate)
				|| !Holidays(context, currentDate);
		}

	}
}