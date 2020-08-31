using MagicConsole.DataLogics.Notification;
using MagicConsole.Utils.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MagicConsole.DataLogics.Pilot.Notifikasi
{
    class NotifikasiPilot
    {
        public static void getPilotNotification(string status)
        {
            var getData = PilotInformationDAL.getDataPilotAvailabe(status);
            var data = getData.ToList();

            if (data.Count > 0)
            {
                data.ForEach(item =>
                {
                    if (status == "SPK1")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "SPK KAPAL " + item.nama_kapal + " SUDAH TERBIT UNTUK RENCANA PELAYANAN PANDU PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ". SILAHKAN UNTUK MENGURUS IJIN GERAK.";
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.id_master_area); param.Add("kd_regional", item.kd_regional);
                        param.Add("regional", item.regional_nama); param.Add("terminal", item.kawasan); param.Add("no_ppk1", item.no_ppk1); param.Add("pelabuhan_asal", item.from_mdmg_nama); param.Add("pelabuhan_tujuan", item.to_mdmg_nama);
                        param.Add("kd_agen", item.kd_agen); param.Add("message", message); param.Add("nama_kapal", item.nama_kapal);
                        param.Add("status", "SPK1"); param.Add("title", "Pilot Information - " + item.kawasan);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "SPK1", "99998", data, "Pilot Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Pilot", param, notif_id);
                        Console.WriteLine(res + "(" + status + " PILOT INFORMATION)");
                    }
                    else if (status == "PENETAPAN")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "RENCANA PEMANDUAN KAPAL " + item.nama_kapal + " AKAN DILAYANI PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ".";
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.id_master_area); param.Add("kd_regional", item.kd_regional);
                        param.Add("regional", item.regional_nama); param.Add("terminal", item.kawasan); param.Add("no_ppk1", item.no_ppk1); param.Add("pelabuhan_asal", item.from_mdmg_nama); param.Add("pelabuhan_tujuan", item.to_mdmg_nama);
                        param.Add("kd_agen", item.kd_agen); param.Add("message", message); param.Add("nama_kapal", item.nama_kapal);
                        param.Add("status", "PENETAPAN"); param.Add("title", "Pilot Information - " + item.kawasan);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "PENETAPAN", "99998", data, "Pilot Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Pilot", param, notif_id);
                        Console.WriteLine(res + "(" + status + " PILOT INFORMATION)");
                    }
                    else if (status == "PERMOHONAN")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_permohonan, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "PERMOHONAN PELAYANAN PANDU KAPAL " + item.nama_kapal + " PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ".";
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.id_master_area); param.Add("kd_regional", item.kd_regional);
                        param.Add("regional", item.regional_nama); param.Add("terminal", item.kawasan); param.Add("no_ppk1", item.no_ppk1); param.Add("pelabuhan_asal", item.from_mdmg_nama); param.Add("pelabuhan_tujuan", item.to_mdmg_nama);
                        param.Add("kd_agen", item.kd_agen); param.Add("message", message); param.Add("nama_kapal", item.nama_kapal);
                        param.Add("status", "PERMOHONAN"); param.Add("title", "Pilot Information - " + item.kawasan);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "PERMOHONAN", "99998", data, "Pilot Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Pilot", param, notif_id);
                        Console.WriteLine(res + "(" + status + " PILOT INFORMATION)");
                    }
                    else if (status == "AKAN DILAYANI")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "RENCANA PEMANDUAN KAPAL " + item.nama_kapal + " AKAN DILAYANI PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ".";
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.id_master_area); param.Add("kd_regional", item.kd_regional);
                        param.Add("regional", item.regional_nama); param.Add("terminal", item.kawasan); param.Add("no_ppk1", item.no_ppk1); param.Add("pelabuhan_asal", item.from_mdmg_nama); param.Add("pelabuhan_tujuan", item.to_mdmg_nama);
                        param.Add("kd_agen", item.kd_agen); param.Add("message", message); param.Add("nama_kapal", item.nama_kapal);
                        param.Add("status", "AKAN DILAYANI"); param.Add("title", "Pilot Information - " + item.kawasan);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "AKAN DILAYANI", "99998", data, "Pilot Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Pilot", param, notif_id);
                        Console.WriteLine(res + "(" + status + " PILOT INFORMATION)");
                    }
                    else if (status == "MELAMPAUI TGL PELAYANAN")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "PERMOHONAN PELAYANAN PANDU KAPAL " + item.nama_kapal + " SUDAH MELAMPAUI BATAS PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ".";
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.id_master_area); param.Add("kd_regional", item.kd_regional);
                        param.Add("regional", item.regional_nama); param.Add("terminal", item.kawasan); param.Add("no_ppk1", item.no_ppk1); param.Add("pelabuhan_asal", item.from_mdmg_nama); param.Add("pelabuhan_tujuan", item.to_mdmg_nama);
                        param.Add("kd_agen", item.kd_agen); param.Add("message", message); param.Add("nama_kapal", item.nama_kapal);
                        param.Add("status", "MELAMPAUI PERMOHONAN"); param.Add("title", "Pilot Information - " + item.kawasan);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        int notif_id = id.Next(10000, 99999);
                        string insertNotification = Notifications.insertNotification(message, "MELAMPAUI TGL PELAYANAN", "99998", data, "Pilot Information", 0, notif_id);

                        var res = Notifications.sendNotification("SPECIFIC", "Pilot", param, notif_id);
                        Console.WriteLine(res + "(" + status + " PILOT INFORMATION)");
                    }
                    Console.ResetColor();
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BELUM ADA NOTIFIKASI DIKIRIMKAN (" + status + " PILOT INFORMATION)");
                Console.ResetColor();
            }
        }
    }
}
