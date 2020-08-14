using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using MagicConsole.DataLogics.Terminal;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using MagicConsole.DataLogics.Notification;
using MagicConsole.Utils.PrettyDate;
using MagicConsole.Utils.PrettyDate.lib;
using MagicConsole.DataLogics.Terminal.Notifikasi;
using MagicConsole.DataLogics.Passanger;
using MagicConsole.DataLogics.Notifikasi.Passanger;
using MagicConsole.DataLogics.Pilot;
using MagicConsole.DataLogics.Pilot.Notifikasi;

namespace MagicConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread check_every_one_minutes = new Thread(() =>
            {
                while (true)
                {
                    // Notifikasi terminal
                    NotifikasiTerminal.getTerminalNotification("RENCANA");
                    NotifikasiTerminal.getTerminalNotification("SANDAR");
                    NotifikasiTerminal.getTerminalNotification("HISTORY");
                    NotifikasiTerminal.getTerminalNotification("AKAN KELUAR");

                    // Notifikasi penumpang
                    NotifikasiPassanger.getPassangerNotification("RENCANA");
                    NotifikasiPassanger.getPassangerNotification("SANDAR");
                    NotifikasiPassanger.getPassangerNotification("HISTORY");
                    NotifikasiPassanger.getPassangerNotification("AKAN KELUAR");

                    // Notifikasi pilot
                    NotifikasiPilot.getPilotNotification("PERMOHONAN");
                    NotifikasiPilot.getPilotNotification("PENETAPAN");
                    NotifikasiPilot.getPilotNotification("SPK1");
                    NotifikasiPilot.getPilotNotification("AKAN DILAYANI");

                    Thread.Sleep(1000 * 60);
                }
            });

            Thread check_every_n_minutes = new Thread(() =>
            {
                while (true)
                {
                    // Notifikasi terminal
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA KELUAR");
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA SANDAR");

                    // Notifikasi pilot
                    NotifikasiPilot.getPilotNotification("MELAMPAUI TGL PELAYANAN");

                    Thread.Sleep(1000 * 60 * 20);
                }
            });

            check_every_one_minutes.Start();
            check_every_n_minutes.Start();

        }

    }
}
    