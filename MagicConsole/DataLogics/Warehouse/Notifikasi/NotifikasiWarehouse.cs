﻿using MagicConsole.DataLogics.Notification;
using MagicConsole.Utils.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MagicConsole.DataLogics.Warehouse.Notifikasi
{
    class NotifikasiWarehouse
    {
        public static void getWarehouseNotification(string status)
        {
            var getData = WarehouseInformationDAL.getDataWarehouseAvailabe(status);
            var data = getData.ToList();

            if (data.Count > 0)
            {
                data.ForEach(item =>
                {
                    if (status == "MEMULAI TUMPUKAN")
                    {
                        DateTime date = DateTime.ParseExact(item.created_date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "PENUMPUKAN " + item.nama_barang + " DI " + item.nama_terminal + " PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + " JUMLAH PENUMPUKAN " + item.jumlah_real + " (ton) NAMA KAPAL " + item.nama_kapal;

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_region); param.Add("kd_terminal", item.kd_terminal); 
                        param.Add("pelanggan", item.pelanggan); param.Add("message", message);
                        param.Add("regional", item.nama_regional); param.Add("terminal", item.nama_terminal); param.Add("mglap_nama", item.mglap_nama); param.Add("nama_kapal", item.nama_kapal); param.Add("nama_vak", item.nama_vak);
                        param.Add("occupied", item.occupied); param.Add("nama_barang", item.nama_barang); param.Add("lama_tumpuk", item.lama_tumpuk); param.Add("tgl_mulai", date.AddDays(-20).ToString("yyyy-MM-dd HH:mm")); param.Add("created_date", date.ToString("yyyy-MM-dd HH:mm"));
                        param.Add("status", "MEMULAI TUMPUKAN"); param.Add("title", "Warehouse Information - " + item.nama_terminal+ "/" + item.mglap_nama);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "MEMULAI TUMPUKAN", "99998", data, "Warehouse Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Warehouse", param, notif_id);
                        Console.WriteLine(res + "(" + status + " WAREHOUSE INFORMATION)");
                    }
                    else if (status == "20 HARI TUMPUKAN")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime now = DateTime.Now;
                        TimeSpan diff = now - date;
                        var message = "MASA PENUMPUKAN " + item.nama_barang + " DI " + item.nama_terminal + " SUDAH MEMASUKI HARI KE " + diff.Days + " NAMA KAPAL " + item.nama_kapal;

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_region); param.Add("kd_terminal", item.kd_terminal);
                        param.Add("pelanggan", item.pelanggan); param.Add("message", message);
                        param.Add("regional", item.nama_regional); param.Add("terminal", item.nama_terminal); param.Add("mglap_nama", item.mglap_nama); param.Add("nama_kapal", item.nama_kapal); param.Add("nama_vak", item.nama_vak);
                        param.Add("occupied", item.occupied); param.Add("nama_barang", item.nama_barang); param.Add("lama_tumpuk", item.lama_tumpuk); param.Add("tgl_mulai", date.AddDays(-20).ToString("yyyy-MM-dd HH:mm")); param.Add("created_date", date.ToString("yyyy-MM-dd HH:mm"));
                        param.Add("status", "20 HARI TUMPUKAN"); param.Add("title", "Warehouse Information - " + item.nama_terminal + "/" + item.mglap_nama);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "20 HARI TUMPUKAN", "99998", data, "Warehouse Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Warehouse", param, notif_id);
                        Console.WriteLine(res + "(" + status + " WAREHOUSE INFORMATION)");
                    }

                    Console.ResetColor();
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BELUM ADA NOTIFIKASI DIKIRIMKAN (" + status + " WAREHOUSE INFORMATION)");
                Console.ResetColor();
            }
        }
    }
}
