using McDonalds.commun.Constants;
using McDonalds.Commun;
using McDonalds.Commun.Helpers;
using McDonalds.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
    public class DeploiementDateRestriction
    {
        public static bool CheckDeploiementDate(int restaurantId, DateTime currentDate)
        {
            
            DeploimentDateModel model = new DeploimentDateModel()
            {
                RestaurantId = restaurantId,
            };

            DataTable deploiementTable = ExcelParserHelper
                .ReadExcelFile
                (
                    AppSettings.ReadSetting<string>(AppSettingConstants.ListRestaurantFile, default(string))
                ).Tables["Déploiement"];



            for (int i = 2; i < deploiementTable.Rows.Count; i++)
            {
                if (Convert.ToInt32(deploiementTable.Rows[i][0]) == restaurantId)
                {
                    for (int j = 1; j < deploiementTable.Columns.Count; j++)
                    {
                        if (deploiementTable.Rows[i][j] is DateTime && ((DateTime)deploiementTable.Rows[i][j]).Date == currentDate.Date)
                        {
                            model.DeploimentDate = Convert.ToDateTime(deploiementTable.Rows[i][j]);
                            break;
                        }
                    }
                    break;
                }
            };

            return model.DeploimentDate.HasValue;
        }
    }
}