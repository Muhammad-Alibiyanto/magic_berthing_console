using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Utils.Date
{
    class MonthFormatter
    {
        public static string getMonthName(int month_number)
        {
            string month_name = null;
            switch (month_number)
            {
                case 1:
                    month_name = "Januari";
                    break;
                case 2:
                    month_name = "Februari";
                    break;
                case 3:
                    month_name = "Maret";
                    break;
                case 4:
                    month_name = "April";
                    break;
                case 5:
                    month_name = "Mei";
                    break;
                case 6:
                    month_name = "Juni";
                    break;
                case 7:
                    month_name = "Juli";
                    break;
                case 8:
                    month_name = "Agustus";
                    break;
                case 9:
                    month_name = "September";
                    break;
                case 10:
                    month_name = "Oktober";
                    break;
                case 11:
                    month_name = "November";
                    break;
                case 12:
                    month_name = "Desember";
                    break;
            }

            return month_name;
        }
    }
}
