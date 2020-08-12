using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Utils.PrettyDate.lib
{
    class Duration
    {
        public static int Durations(TimeSpan date, string unit)
        {
            int response = 0;
            TimeSpan interval = new TimeSpan(date.Days, date.Hours, date.Minutes, date.Seconds, date.Milliseconds);

            if (unit == "d")
            {
                response = interval.Days;
            }
            else if(unit == "h")
            {
                response = interval.Hours;
            }
            else if (unit == "m")
            {
                response = interval.Minutes;
            }
            else if (unit == "s")
            {
                response = interval.Seconds;
            }

            return response;
        }
    }
}
