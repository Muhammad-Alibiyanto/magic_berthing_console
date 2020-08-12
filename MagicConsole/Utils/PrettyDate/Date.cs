using MagicConsole.Utils.PrettyDate.lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Utils.PrettyDate
{
    class Date
    {
        public static TimeSpan getDate(DateTime date)
        {
            TimeSpan diff = Diff.getDiff(date);
            return diff;
        }

        public static int getInterval(TimeSpan date, string unit)
        {
            int interval = Duration.Durations(date, unit);
            return interval;
        }
    }
}
