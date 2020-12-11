using McDonalds.Data.Context;
using McDonalds.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace McDonalds.Domain
{
    public class TwoWeekRestartRestriction
    {
		public static bool CheckLastServerUpTimes(McDonaldsContext context, int restaurantId, DateTime upTimes)
        {
			return context
					.ServerEvents
					.Any(r => r.RestaurantId == restaurantId && r.UpTimes == upTimes);
		}
        public static bool IsValid(McDonaldsContext context, int restaurantId, DateTime dateTime)
		{

			DateTime days = dateTime.Date.AddDays(-15);

			return context
				.ServerEvents
				.Where(r => r.RestaurantId == restaurantId && r.Event == Event.RedemarrageOK && r.UpTimes < days)
				.OrderByDescending(r => r.Date)
				.FirstOrDefault() != null;
		}
	}
}