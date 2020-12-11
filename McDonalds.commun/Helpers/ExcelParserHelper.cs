using ExcelDataReader;
using System;
using System.Data;
using System.IO;

namespace McDonalds.Commun.Helpers
{
    public class ExcelParserHelper
    {
        //public static DeploimentDateModel CheckDeploimentDateFromExcelFile(string fileName, int restaurantId, DateTime currentDate)
        //{
        //    // exemple de la case 32 comme date de deploiement
        //    DeploimentDateModel model = new DeploimentDateModel()
        //    {
        //        RestaurantId = restaurantId,
        //    };

        //    #region Excel

        //    FileInfo file = new FileInfo(fileName);

        //    string nomFichier = file.FullName;
        //    DataSet dsExcelTest = null;

        //    IExcelDataReader excelReader = null;

        //    try
        //    {
        //        FileStream stream = File.Open(nomFichier, FileMode.Open, FileAccess.Read);

        //        excelReader = Path.GetExtension(nomFichier).ToLower() == ".xls" 
        //            ? ExcelReaderFactory.CreateBinaryReader(stream) 
        //            : ExcelReaderFactory.CreateOpenXmlReader(stream);

        //        dsExcelTest = excelReader.AsDataSet();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (excelReader != null && !excelReader.IsClosed)
        //            excelReader.Close();
        //    }
            
        //    DataTable deploiementTable = dsExcelTest.Tables["Déploiement"];

            

        //    for (int i = 2; i < deploiementTable.Rows.Count; i++)
        //    {
        //        if ( Convert.ToInt32(deploiementTable.Rows[i][0]) == restaurantId)
        //        {
        //            for (int j = 1; j < deploiementTable.Columns.Count; j++)
        //            {
        //                if (deploiementTable.Rows[i][j] is DateTime && ((DateTime)deploiementTable.Rows[i][j]).Date == currentDate.Date)
        //                {
        //                    model.DeploimentDate = Convert.ToDateTime(deploiementTable.Rows[i][j]);
        //                    break;
        //                }
        //            }
        //            break;
        //        }
        //    };

        //    #endregion

        //    return model;
        //}

        public static DataSet ReadExcelFile(string fileName)
        {
            #region Excel

            FileInfo file = new FileInfo(fileName);

            string nomFichier = file.FullName;

            IExcelDataReader excelReader = null;

            try
            {
                FileStream stream = File.Open(nomFichier, FileMode.Open, FileAccess.Read);

                excelReader = Path.GetExtension(nomFichier).ToLower() == ".xls"
                    ? ExcelReaderFactory.CreateBinaryReader(stream)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);

                return excelReader.AsDataSet();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (excelReader != null && !excelReader.IsClosed)
                    excelReader.Close();
            }

            #endregion Excel

        }
    }
}