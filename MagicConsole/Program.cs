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
using MagicConsole.DataLogics.Container;
using MagicConsole.Model.Container;
using MagicConsole.DataLogics.Container.Notifikasi;

namespace MagicConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            DateTime date = DateTime.Now;

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

                    NotifikasiContainer.getContainerNotification("MEMULAI TUMPUKAN");

                    Thread.Sleep(1000 * 60); // 1 minutes interval
                }
            });

            /*Thread second = new Thread(() =>
            {
                while (true)
                {
                    

                    Thread.Sleep(1000 * 60 * 20); // 20 minutes check
                }
            });*/

            Thread third = new Thread(() =>
            {
                while (true)
                {
                    // Notifikasi terminal
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA KELUAR");
                    NotifikasiTerminal.getTerminalNotification("MELAMPAUI RENCANA SANDAR");

                    // Notifikasi pilot
                    NotifikasiPilot.getPilotNotification("MELAMPAUI TGL PELAYANAN");

                    // Notifikasi warehouse
                    NotifikasiWarehouse.getWarehouseNotification("20 HARI TUMPUKAN");
                    NotifikasiContainer.getContainerNotification("15 HARI TUMPUKAN");

                    Thread.Sleep((1000 * 60 * 60) * 24); // 24 hours check
                }
            });

            // Running this task only when now is between 00:08 and 00:12 OR between 08:08 and 08:12 OR between 16:08 and 16:12
            if( (Convert.ToInt32(date.ToString("HHmm")) >= 0009 && Convert.ToInt32(date.ToString("HHmm")) <= 0011) || (Convert.ToInt32(date.ToString("HHmm")) >= 0809 && Convert.ToInt32(date.ToString("HHmm")) <= 0811) || (Convert.ToInt32(date.ToString("HHmm")) >= 1609 && Convert.ToInt32(date.ToString("HHmm")) <= 1611) )
            {
                Thread fourth = new Thread(() =>
                {
                    while (true)
                    {
                        NotifikasiKegiatanTerminal.getTerminalKegiatanNotification();

                        Thread.Sleep(1000 * 60); // 1 minutes interval
                    }
                });

                fourth.Start();
            }

            first.Start();
            //second.Start();
            third.Start();

            //Console.WriteLine( (Convert.ToInt32(date.ToString("HHmm")) >= 1540 && Convert.ToInt32(date.ToString("HHmm")) <= 1546) || (Convert.ToInt32(date.ToString("HHmm")) >= 1000 && Convert.ToInt32(date.ToString("HHmm")) <= 2000));
        }

    }
}
    