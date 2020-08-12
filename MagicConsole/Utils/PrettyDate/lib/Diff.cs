using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Utils.PrettyDate.lib
{
    class Diff
    {
        public static TimeSpan getDiff(DateTime date)
        {
            TimeSpan diff = DateTime.Now.Subtract(date);
            return diff;
        }
    }
}
