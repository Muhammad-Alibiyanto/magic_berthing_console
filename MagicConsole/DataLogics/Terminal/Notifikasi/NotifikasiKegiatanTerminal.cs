using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicConsole.DataLogics.Terminal.Notifikasi
{
    class NotifikasiKegiatanTerminal
    {
        public static void getTerminalKegiatanNotification()
        {
            var notification = TerminalInformationKegiatanDAL.getBoatTerminalDetail();
           
            Console.WriteLine(notification);
            Console.ResetColor();
                
        }
    }
}
