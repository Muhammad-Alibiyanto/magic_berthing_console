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
using MagicConsole.DataLogics.Warehouse.Notifikasi;
using System.Reflection.Metadata.Ecma335;

namespace MagicConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread first = new Thread(() =>
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

                    // Notifikasi warehouse
                    NotifikasiWarehouse.getWarehouseNotification("MEMULAI TUMPUKAN");

                    Thread.Sleep(1000 * 60); // 1 minutes check
                }
            });

            Thread second = new Thread(() =>
            {
                while (true)
                {
                    // Notifikasi terminal
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA KELUAR");
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA SANDAR");

                    // Notifikasi pilot
                    NotifikasiPilot.getPilotNotification("MELAMPAUI TGL PELAYANAN");

                    Thread.Sleep(1000 * 60 * 20); // 20 minutes check
                }
            });

            Thread third = new Thread(() =>
            {
                while (true)
                {
                    // Notifikasi warehouse
                    NotifikasiWarehouse.getWarehouseNotification("20 HARI TUMPUKAN");

                    Thread.Sleep((1000 * 60 * 60) * 24); // 24 hours check
                }
            });

            first.Start();
            second.Start();
            third.Start();
        }

    }
}
    