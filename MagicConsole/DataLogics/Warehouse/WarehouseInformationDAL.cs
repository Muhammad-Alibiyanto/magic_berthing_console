using Dapper;
using MagicConsole.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace MagicConsole.DataLogics.Warehouse
{
    class WarehouseInformationDAL
    {
        public static IEnumerable<WarehouseAvailable> getDataWarehouseAvailabe(string status)
        {
            IEnumerable<WarehouseAvailable> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramTgl = "";
                    DateTime date = DateTime.Now;
                    //DateTime date = DateTime.ParseExact("2020-08-22 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    if (status == "MEMULAI TUMPUKAN")
                    {
                        paramTgl = " WHERE CREATED_DATE IS NOT NULL AND TO_CHAR(CREATED_DATE, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                    }
                    else if (status == "20 HARI TUMPUKAN")
                    {
                        paramTgl = " WHERE TGL_MULAI IS NOT NULL AND TO_CHAR(TGL_MULAI, 'YYYY-MM-DD HH24:MI') < '" + date.AddDays(-20).ToString("yyyy-MM-dd HH:mm") + "'";
                    }

                    string sql = "SELECT * FROM T_STORAGE_CARGO_DETAIL" + paramTgl;

                    result = connection.Query<WarehouseAvailable>(sql);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
