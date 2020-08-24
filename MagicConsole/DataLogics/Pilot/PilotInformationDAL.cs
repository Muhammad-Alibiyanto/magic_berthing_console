using Dapper;
using MagicConsole.Model.Pilot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace MagicConsole.DataLogics.Pilot
{
    class PilotInformationDAL
    {
        public static IEnumerable<AvailablePilot> getDataPilotAvailabe(string status)
        {
            IEnumerable<AvailablePilot> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramStatus = "";
                    string paramTgl = "";
                    DateTime date = DateTime.Now;
                    //DateTime date = DateTime.ParseExact("2020-08-01 11:42:16", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    if (status == "PERMOHONAN")
                    {
                        paramTgl = " AND CREATED_PERMOHONAN IS NOT NULL AND TO_CHAR(CREATED_PERMOHONAN, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = " WHERE STATUS='PERMOHONAN'";
                    }
                    else if (status == "PENETAPAN")
                    {
                        paramTgl = " AND CREATED_PENETAPAN IS NOT NULL AND TO_CHAR(CREATED_PENETAPAN, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = " WHERE STATUS='PENETAPAN'";
                    }
                    else if (status == "SPK1")
                    {
                        paramTgl = " AND CREATED_SPKP IS NOT NULL AND TO_CHAR(CREATED_SPKP, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = " WHERE STATUS='SPK1'";
                    }
                    else if(status == "AKAN DILAYANI")
                    {
                        paramTgl = " AND TGL_MULAI IS NOT NULL AND TO_CHAR(TGL_MULAI, 'YYYY-MM-DD HH24:MI') = '" + date.AddMinutes(30).ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = " WHERE STATUS IN ('PERMOHONAN', 'PENETAPAN', 'SPK1')";
                    }
                    else if (status == "MELAMPAUI TGL PELAYANAN")
                    {
                        paramStatus = " WHERE STATUS IN ('PERMOHONAN', 'PENETAPAN', 'SPK1')";
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "'";
                    }

                    string sql = "SELECT * FROM (" +
                                    "SELECT * FROM VW_MAGIC_PILOT_INFORMATION " + paramStatus + paramTgl + 
                                   ")";


                    result = connection.Query<AvailablePilot>(sql);
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
