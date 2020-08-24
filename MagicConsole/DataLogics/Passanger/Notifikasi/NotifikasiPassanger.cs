using MagicConsole.DataLogics.Notification;
using MagicConsole.DataLogics.Passanger;
using MagicConsole.DataLogics.Terminal;
using MagicConsole.Model.Notification;
using MagicConsole.Utils.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MagicConsole.DataLogics.Notifikasi.Passanger
{
    class NotifikasiPassanger
    {
        public static void getPassangerNotification(string status)
        {
            var getData = PassangerInformationDAL.getDataPassangerAvailabe(status);
            var data = getData.ToList();

            if (data.Count > 0)
            {
                data.ForEach(item =>
                {
                    if (status == "RENCANA")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai_ptp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "KAPAL " + item.nama_kapal + " RENCANA SANDAR DI " + item.kawasan + " (" + item.nama_lokasi + ") PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm");
                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "RENCANA"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "RENCANA", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION) " + insertNotification);
                    }
                    else if (status == "SANDAR")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "KAPAL " + item.nama_kapal + " SUDAH SANDAR DI " + item.kawasan + " (" + item.nama_lokasi + ") PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm");

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "SANDAR"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "SANDAR", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION)");
                    }
                    else if (status == "AKAN KELUAR")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_selesai_ptp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "KAPAL " + item.nama_kapal + " RENCANA KELUAR DARI " + item.kawasan + " (" + item.nama_lokasi + ") PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm");

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "SANDAR"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "AKAN KELUAR", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION)");
                    }
                    else if (status == "HISTORY")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_selesai, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "KAPAL " + item.nama_kapal + " TELAH LEPAS LANDAS DARI " + item.kawasan + " (" + item.nama_lokasi + ") PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm");

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "HISTORY"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "HISTORY", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION)");
                    }
                    else if (status == "MELAMPAUI RENCANA SANDAR")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_mulai_ptp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "RENCANA SANDAR KAPAL " + item.nama_kapal + " SUDAH MELEWATI BATAS PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ". SILAHKAN LAKUKAN PERUBAHAN PERENCANAAN SANDAR";

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "RENCANA"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "MELAMPAUI RENCANA SANDAR", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION)");
                    }
                    else if (status == "MELAMPAUI RENCANA KELUAR")
                    {
                        DateTime date = DateTime.ParseExact(item.tgl_selesai_ptp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string month_name = MonthFormatter.getMonthName(date.Month);
                        var message = "RENCANA LEPAS TAMBAT KAPAL " + item.nama_kapal + " SUDAH MELEWATI BATAS PADA " + date.ToString("dd") + " " + month_name.ToUpper() + " " + date.ToString("yyyy") + " JAM " + date.ToString("HH:mm") + ". SILAHKAN AJUKAN PERPANJANGAN MASA TAMBAT.";

                        Dictionary<String, String> param = new Dictionary<String, String>();
                        param.Add("kd_cabang", item.kd_cabang); param.Add("kd_cabang_induk", item.kd_cabang_induk); param.Add("kd_regional", item.kd_regional);
                        param.Add("kd_terminal", item.kd_terminal); param.Add("no_ppk_jasa", item.no_ppk_jasa); param.Add("message", message);
                        param.Add("status", "SANDAR"); param.Add("kd_agen", item.kode_agen); param.Add("title", "Passanger Information - " + item.kawasan + "/" + item.nama_lokasi);

                        string data = JsonSerializer.Serialize(param);
                        Random id = new Random();
                        string insertNotification = Notifications.insertNotification(message, "MELAMPAUI RENCANA KELUAR", "99998", data, "Passanger Information", 0, id.Next(10000, 99999));

                        var res = Notifications.sendNotification("GLOBAL", "Passanger", param);
                        Console.WriteLine(res + "(" + status + " PASSANGER INFORMATION)");
                    }
                    Console.ResetColor();
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BELUM ADA NOTIFIKASI DIKIRIMKAN (" + status + " PASSANGER INFORMATION)");
                Console.ResetColor();
            }
        }
    }
}
