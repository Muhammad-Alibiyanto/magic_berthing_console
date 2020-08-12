using Dapper;
using MagicConsole.Model.Terminal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace MagicConsole.DataLogics.Terminal
{
    class TerminalInformationDAL
    {
        public static IEnumerable<TerminalAvailable> getDataTerminalAvailabe(string status)
        {
            IEnumerable<TerminalAvailable> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramStatus = "";
                    string paramTgl = "";
                    DateTime date = DateTime.Now;

                    if (status == "HISTORY")
                    {
                        paramTgl = " AND TGL_MULAI IS NOT NULL AND TO_CHAR(TGL_SELESAI, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "' AND STATUS_NOTA=1";
                        paramStatus = "HISTORY";
                    }
                    else if (status == "SANDAR")
                    {
                        paramTgl = " AND TGL_MULAI IS NOT NULL AND TO_CHAR(TGL_MULAI, 'YYYY-MM-DD HH24:MI') = '" + date.ToString("yyyy-MM-dd HH:mm") + "' AND TGL_SELESAI IS NULL AND STATUS_NOTA=0";
                        paramStatus = "SANDAR";
                    }
                    else if(status == "AKAN KELUAR")
                    {
                        paramTgl = " AND TGL_MULAI IS NOT NULL AND TO_CHAR(TGL_SELESAI_PTP, 'YYYY-MM-DD HH24:MI') = '" + date.AddMinutes(30).ToString("yyyy-MM-dd HH:mm") + "' AND TGL_SELESAI IS NULL AND STATUS_NOTA=0";
                        paramStatus = "SANDAR";
                    }
                    else if (status == "RENCANA")
                    {
                        paramStatus = "RENCANA";
                        paramTgl = " AND TO_CHAR(TGL_MULAI_PTP, 'YYYY-MM-DD HH24:MI') = '" + date.AddMinutes(30).ToString("yyyy-MM-dd HH:mm") + "' AND TGL_MULAI IS NULL AND TGL_SELESAI IS NULL AND STATUS_NOTA=0";
                    }
                    else if (status == "MELAMPAUI RENCANA SANDAR")
                    {
                        paramStatus = "RENCANA";
                        paramTgl = " AND TO_CHAR(TGL_MULAI_PTP, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "' AND TGL_MULAI IS NULL AND TGL_SELESAI IS NULL AND STATUS_NOTA=0";
                    }
                    else if (status == "MELAMPAUI RENCANA KELUAR")
                    {
                        paramStatus = "SANDAR";
                        paramTgl = " AND TO_CHAR(TGL_SELESAI_PTP, 'YYYY-MM-DD HH24:MI') < '" + date.AddMinutes(5).ToString("yyyy-MM-dd HH:mm") + "' AND TGL_MULAI IS NOT NULL AND TGL_SELESAI IS NULL AND STATUS_NOTA=0";
                    }

                    string sql = "SELECT * FROM(" +
                        "SELECT * FROM (SELECT A.*, B.REGIONAL_NAMA NAMA_REGIONAL, " +
                        "(CASE WHEN A.TGL_MULAI IS NULL AND A.TGL_SELESAI IS NULL THEN 'RENCANA' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NULL THEN 'SANDAR' " +
                        "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NOT NULL THEN 'HISTORY' END" +
                        ") STATUS " +
                        "FROM VW_MAGIC_TRMNL_INFO_ALL A, APP_REGIONAL B WHERE A.KD_REGIONAL=B.ID AND B.PARENT_ID IS NULL AND B.ID NOT IN (12300000,20300001) )" +
                    ") WHERE STATUS = '" + paramStatus + "'" + paramTgl;


                    result = connection.Query<TerminalAvailable>(sql);
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
