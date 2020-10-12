using Dapper;
using MagicConsole.Model.Container;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace MagicConsole.DataLogics.Container
{
    class ContainerInformationDAL
    {
        public static IEnumerable<ContainerData> getContainerAvailableData(string status)
        {
            IEnumerable<ContainerData> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    string paramTgl = "";
                    DateTime date = DateTime.Now;
                    //DateTime date = DateTime.ParseExact("2020-09-13 02:45:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    if (status == "MEMULAI TUMPUKAN")
                    {
                        paramTgl = " WHERE TRANSACT_DATE IS NOT NULL AND TO_CHAR(TRANSACT_DATE, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                    }
                    else if (status == "15 HARI TUMPUKAN")
                    {
                        paramTgl = " WHERE LAMA_PENUMPUKAN_RECV > 360 OR LAMA_PENUMPUKAN_DISC > 360";
                    }

                    var sql = @"SELECT * FROM (SELECT T_STORAGE_CONTAINER_BOX_DETAIL.*, APP_REGIONAL.REGIONAL_NAMA FROM T_STORAGE_CONTAINER_BOX_DETAIL JOIN APP_REGIONAL ON T_STORAGE_CONTAINER_BOX_DETAIL.KD_REGIONAL=APP_REGIONAL.ID AND APP_REGIONAL.PARENT_ID IS NULL AND APP_REGIONAL.ID NOT IN (12300000,20300001))" + paramTgl;

                    result = connection.Query<ContainerData>(sql);
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
