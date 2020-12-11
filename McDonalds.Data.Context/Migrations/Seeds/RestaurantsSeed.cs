using McDonalds.commun.Constants;
using McDonalds.Commun;
using McDonalds.Commun.Helpers;
using McDonalds.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Context.Migrations.Seeds
{
    public class RestaurantsSeed
    {
        public static void Restaurants(McDonaldsContext context)
        {
            List<Restaurant> restaurantList = new List<Restaurant>();

            try
            {
                DataTable deploiementTable = ExcelParserHelper
                        .ReadExcelFile(@"" +AppSettings.ReadSetting<string>(AppSettingConstants.ListRestaurantFile, default(string)))
                        .Tables["Déploiement"];

                for (int i = 2; i < deploiementTable.Rows.Count; i++)
                {
                    restaurantList.Add(new Restaurant()
                    {
                        RestaurantId = Convert.ToInt32(deploiementTable.Rows[i][0]),
                        ServerIpAddress = IpAddressHelper.CcToIp(Convert.ToInt32(deploiementTable.Rows[i][0]),71).ToString(),
                        Nom = Convert.ToString(deploiementTable.Rows[i][1]),
                    });
                }

                restaurantList.Add(new Restaurant()
                {
                    RestaurantId = 9,
                    ServerIpAddress = "10.19.9.71",
                    Nom = "ParisC",
                });

                context.Set<Restaurant>().AddOrUpdate(r => r.RestaurantId, restaurantList.Where(r => r.RestaurantId != 0).ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}