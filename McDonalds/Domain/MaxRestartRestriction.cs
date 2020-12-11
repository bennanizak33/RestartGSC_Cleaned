using McDonalds.commun.Constants;
using McDonalds.Commun;
using McDonalds.Data.Context;
using McDonalds.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
	public class MaxRestartRestriction
	{
		public static bool IsValid(McDonaldsContext context, DateTime dateTime)
		{
			DateTime dateDebut = dateTime.Date;
			DateTime dateFin = dateTime.Date.AddDays(1);


			return context
				.ServerEvents
				.AsNoTracking()
				.Where(se => se.Date > dateDebut && se.Date < dateFin && se.Event == Event.RedemarrageOK)
				.Count() < AppSettings.ReadSetting(AppSettingConstants.MaxRestartPerDay, 200);
		}
    }
}