using McDonalds.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Context.Migrations.Seeds
{
    public class HolydaysSeed
    {
        public static void Holydays(McDonaldsContext context)
        {
            context.Set<Holyday>().AddOrUpdate(h => h.Code, new List<Holyday>()
            {
                new Holyday
                {
                    HolydayId = 1,
                    Code = "1erJanvier",
                    Label = "1er janvier",
                    Date = DateTime.ParseExact("2020-01-01","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 2,
                    Code = "LundiDePaques",
                    Label = "Lundi de Pâques",
                    Date = DateTime.ParseExact("2020-04-13","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 3,
                    Code = "1erMai",
                    Label = "1er mai",
                    Date = DateTime.ParseExact("2020-05-01","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)

                },
                new Holyday
                {
                    HolydayId = 4,
                    Code = "8Mai",
                    Label = "8 mai",
                    Date = DateTime.ParseExact("2020-05-08","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 5,
                    Code = "Ascension",
                    Label = "Ascension",
                    Date = DateTime.ParseExact("2020-05-21","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 6,
                    Code = "LundiDePentecote",
                    Label = "Lundi de Pentecôte",
                    Date = DateTime.ParseExact("2020-06-01","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 7,
                    Code = "14Juillet",
                    Label = "14 juillet",
                    Date = DateTime.ParseExact("2020-07-14","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 8,
                    Code = "Assomption",
                    Label = "Assomption",
                    Date = DateTime.ParseExact("2020-08-15","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 9,
                    Code = "Toussaint",
                    Label = "Toussaint",
                    Date = DateTime.ParseExact("2020-11-01","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 10,
                    Code = "11Novembre",
                    Label = "11 novembre",
                    Date = DateTime.ParseExact("2020-11-11","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
                new Holyday
                {
                    HolydayId = 11,
                    Code = "JourDeNoel",
                    Label = "Jour de Noël",
                    Date = DateTime.ParseExact("2020-12-25","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
                },
            }.ToArray());
        }
    }
}