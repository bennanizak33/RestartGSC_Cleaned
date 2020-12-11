
using McDonalds.Data.Context;
using McDonalds.Data.Models;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
    public class PriorityRestriction
    {
        public static IEnumerable<int> GetPriorityRestaurant(McDonaldsContext context)
        {
            DateTime dateDebut = DateTime.Now.Date;

            DateTime DateFin = dateDebut.AddDays(-1);

            return context
                .ServerEvents
                .Where(se => se.Date > DateFin && se.Date < dateDebut && (se.Event == Event.DemandeRejete || se.Event == Event.RedemarrageNOK))
                .OrderByDescending(se => se.Date)
                .DistinctBy(se => se.RestaurantId)
                .Select(se => se.RestaurantId)
                .Take(200);
        }

        public static bool CheckPriority(McDonaldsContext context, int restaurantId)
        {
            return GetPriorityRestaurant(context).FirstOrDefault() == restaurantId;
        }
    }
}