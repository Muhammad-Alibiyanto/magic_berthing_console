using MagicConsole.DataLogics.Notification;
using MagicConsole.Utils.Date;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MagicConsole.DataLogics.Container.Notifikasi
{
    class NotifikasiContainer
    {
        public static void getContainerNotification(string status)
        {
            var getData = ContainerInformationDAL.getContainerAvailableData(status);
            var data = getData.ToList();

            if (data.Count > 0)
            {
                data.ForEach(item =>
                {
                    if (status == "MEMULAI TUMPUKAN")
                    {
                        if (item.tgl_penumpukan_disc != null && item.tgl_penumpukan_recv != null)
                        {
                            DateTime recv_date = DateTime.ParseExact(item.tgl_penumpukan_recv, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime disc_date = DateTime.ParseExact(item.tgl_penumpukan_disc, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            DateTime use_date = new DateTime();
                            string lama_tumpuk = "";
                            string status_message = "";

                            if(disc_date.Subtract(recv_date).TotalSeconds > 0)
                            {
                                use_date = recv_date;
                                lama_tumpuk = item.lama_penumpukan_recv;
                                status_message = "RECEIVING";
                            }
                            else
                            {
                                use_date = disc_date;
                                lama_tumpuk = item.lama_penumpukan_disc;
                                status_message = "DISCHARGE";
                            }

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " PADA " + use_date.ToString("dd") + " " + month.ToUpper() + " " + use_date.ToString("yyyy") + " JAM " + use_date.ToString("HH:mm") + " BLOK " + item.area + ", " + status_message  + " KAPAL " + item.ves_name;

                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", lama_tumpuk);
                            param.Add("status", "MEMULAI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "MEMULAI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                        else if(item.tgl_penumpukan_disc != null && item.tgl_penumpukan_recv == null)
                        {
                            DateTime use_date = DateTime.ParseExact(item.tgl_penumpukan_disc, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " PADA " + use_date.ToString("dd") + " " + month.ToUpper() + " " + use_date.ToString("yyyy") + " JAM " + use_date.ToString("HH:mm") + " BLOK " + item.area + ", DISCHARGE KAPAL " + item.ves_name;

                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", item.lama_penumpukan_disc);
                            param.Add("status", "MEMULAI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "MEMULAI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                        else if (item.tgl_penumpukan_disc == null && item.tgl_penumpukan_recv != null)
                        {
                            DateTime use_date = DateTime.ParseExact(item.tgl_penumpukan_recv, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " PADA " + use_date.ToString("dd") + " " + month.ToUpper() + " " + use_date.ToString("yyyy") + " JAM " + use_date.ToString("HH:mm") + " BLOK " + item.area + ", RECEIVING KAPAL " + item.ves_name;

                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", item.lama_penumpukan_recv);
                            param.Add("status", "MEMULAI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "MEMULAI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                    }
                    else if (status == "15 HARI TUMPUKAN")
                    {

                        if (item.tgl_penumpukan_disc != null && item.tgl_penumpukan_recv != null)
                        {
                            DateTime recv_date = DateTime.ParseExact(item.tgl_penumpukan_recv, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime disc_date = DateTime.ParseExact(item.tgl_penumpukan_disc, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            DateTime use_date = new DateTime();
                            string lama_tumpuk = "";
                            if (disc_date.Subtract(recv_date).TotalSeconds > 0)
                            {
                                use_date = recv_date;
                                lama_tumpuk = item.lama_penumpukan_recv;

                            }
                            else
                            {
                                use_date = disc_date;
                                lama_tumpuk = item.lama_penumpukan_disc;
                            }

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " BLOK " + item.area + " SUDAH MEMASUKI HARI KE " + Convert.ToInt32(lama_tumpuk) / 24;
                            
                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", lama_tumpuk);
                            param.Add("status", "15 HARI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "15 HARI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                        else if (item.tgl_penumpukan_disc != null && item.tgl_penumpukan_recv == null)
                        {
                            DateTime use_date = DateTime.ParseExact(item.tgl_penumpukan_disc, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " BLOK " + item.area + " SUDAH MEMASUKI HARI KE " + Convert.ToInt32(item.lama_penumpukan_disc) / 24;

                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", item.lama_penumpukan_disc);
                            param.Add("status", "15 HARI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "15 HARI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                        else if (item.tgl_penumpukan_disc == null && item.tgl_penumpukan_recv != null)
                        {
                            DateTime use_date = DateTime.ParseExact(item.tgl_penumpukan_recv, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            string month = MonthFormatter.getMonthName(use_date.Month);

                            var message = "PENUMPUKAN CONTAINER NOMER " + item.container_no + " BLOK " + item.area + " SUDAH MEMASUKI HARI KE " + Convert.ToInt32(item.lama_penumpukan_recv) / 24;

                            Dictionary<String, String> param = new Dictionary<String, String>();
                            param.Add("kd_cabang", item.kd_cabang); param.Add("kd_regional", item.kd_regional); param.Add("kd_terminal", item.kd_terminal);
                            param.Add("pelanggan", item.nama_pelanggan); param.Add("message", message);
                            param.Add("regional", item.regional_nama); param.Add("terminal", item.nama_terminal); param.Add("voyage_no", item.voyage_no); param.Add("nama_kapal", item.ves_name); param.Add("container_no", item.container_no);
                            param.Add("ctr_size", item.ctr_size); param.Add("ctr_type", item.ctr_type); param.Add("transact_date", item.transact_date); param.Add("area", item.area); param.Add("equipment", item.equipment);
                            param.Add("jumlah", item.jumlah); param.Add("tgl_penumpukan_recv", item.tgl_penumpukan_recv); param.Add("tgl_penumpukan_disc", item.tgl_penumpukan_disc); param.Add("lama_penumpukan_disc", item.lama_penumpukan_disc); param.Add("lama_penumpukan_recv", item.lama_penumpukan_recv);
                            param.Add("kd_pbm_recv", item.kd_pbm_disc); param.Add("kd_pbm_disc", item.kd_pbm_disc); param.Add("nama_pbm_recv", item.nama_pbm_recv); param.Add("nama_pbm_disc", item.nama_pbm_disc); param.Add("transaction_date", use_date.ToString("yyyy-MM-dd HH:mm")); param.Add("lama_tumpuk", item.lama_penumpukan_recv);
                            param.Add("status", "15 HARI TUMPUKAN"); param.Add("title", "Container Information - " + item.nama_terminal + "/" + item.area); param.Add("kd_agen", item.kode_pelanggan);

                            string data = JsonSerializer.Serialize(param);
                            Random id = new Random();
                            int notif_id = id.Next(10000, 99999);
                            string insertNotification = Notifications.insertNotification(message, "15 HARI TUMPUKAN", "99998", data, "Container Information", 0, notif_id);

                            var res = Notifications.sendNotification("SPECIFIC", "Container", param, notif_id);
                            Console.WriteLine(res + "(" + status + " CONTAINER INFORMATION)");
                        }
                    }

                    Console.ResetColor();
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BELUM ADA NOTIFIKASI DIKIRIMKAN (" + status + " CONTAINER INFORMATION)");
                Console.ResetColor();
            }
        }
    }
}
