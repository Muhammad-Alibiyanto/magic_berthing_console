using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Utils.PrettyDate.lib
{
    class Calendars
    {
        public static string calendar(int date, string unit)
        {
            string response = null;
            if(date > 0)
            {
                string _unit = unit == "d" ? " hari " : unit == "h" ? " jam " : unit == "m" ? " menit " : " second ";
                response = date + _unit + "yang lalu";
            }
            else
            {
                string _unit = unit == "d" ? " hari " : unit == "h" ? " jam " : unit == "m" ? " menit " : " second ";
                response = -date + _unit + "mendatang";
            }
            return response;
        }
    }
}
