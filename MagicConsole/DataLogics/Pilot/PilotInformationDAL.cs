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

                    if (status == "PERMOHONAN")
                    {
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') = '" + date.AddMinutes(30).ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = "PERMOHONAN";
                    }
                    else if (status == "PENETAPAN")
                    {
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') = '" + date.AddMinutes(30).ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = "PENETAPAN";
                    }
                    else if (status == "SPK1")
                    {
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "'";
                        paramStatus = "SPK1";
                    }
                    else if (status == "MELAMPAUI PERMOHONAN")
                    {
                        paramStatus = "PERMOHONAN";
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "'";
                    }
                    else if (status == "MELAMPAUI PENETAPAN")
                    {
                        paramStatus = "PENETAPAN";
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "'";
                    }
                    else if (status == "MELAMPAUI SPK1")
                    {
                        paramStatus = "SPK1";
                        paramTgl = " AND TGL_WORK IS NOT NULL AND TO_CHAR(TGL_WORK, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "'";
                    }

                    string sql = "SELECT * FROM (" +
                                    "SELECT * FROM VW_MAGIC_PILOT_INFORMATION WHERE STATUS='" + paramStatus + "'" + paramTgl + 
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
