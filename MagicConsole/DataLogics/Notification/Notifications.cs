using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Dapper;
using MagicConsole.Model.Notification;
using MagicConsole.Model.Terminal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicConsole.DataLogics.Notification
{
    class Notifications
    {
        public static string sendNotification(string type, string page, Dictionary<String, String> param, int id)
        {
            string str = null;

            string res = null;

            var notification_data = new
            {
                page = "",
                kd_cabang = "",
                kd_cabang_induk = "",
                kd_regional = "",
                kd_terminal = "",
                kd_agen = "",
                nama_kapal = "",
                pelanggan = "",
                unique = "",
                status = "",
                id = "",
                transact_date = "",
                lama_tumpuk = "",
                created_date = "",
                tgl_mulai = ""
            };

            if (page == "Terminal" || page == "Passanger")
            {
                notification_data = new
                {
                    page = page,
                    kd_cabang = param["kd_cabang"],
                    kd_cabang_induk = param["kd_cabang_induk"],
                    kd_regional = param["kd_regional"],
                    kd_terminal = param["kd_terminal"],
                    kd_agen = "",
                    nama_kapal = "",
                    pelanggan = "",
                    unique = param["no_ppk_jasa"],
                    status = param["status"],
                    id = id.ToString(),
                    transact_date = "",
                    lama_tumpuk = "",
                    created_date = "",
                    tgl_mulai = ""
                };
            }
            else if (page == "Pilot")
            {
                notification_data = new
                {
                    page = page,
                    kd_cabang = param["kd_cabang"],
                    kd_cabang_induk = "",
                    kd_regional = param["kd_regional"],
                    kd_terminal = "",
                    kd_agen = param["kd_agen"],
                    nama_kapal = param["nama_kapal"],
                    pelanggan = "",
                    unique = param["no_ppk1"],
                    status = "RENCANA",
                    id = id.ToString(),
                    transact_date = "",
                    lama_tumpuk = "",
                    created_date = "",
                    tgl_mulai = ""
                };
            }
            else if (page == "Warehouse")
            {
                notification_data = new
                {
                    page = page,
                    kd_cabang = param["kd_cabang"],
                    kd_cabang_induk = "",
                    kd_regional = param["kd_regional"],
                    kd_terminal = param["kd_terminal"],
                    kd_agen = "",
                    nama_kapal = "",
                    pelanggan = param["pelanggan"],
                    unique = param["nama_vak"],
                    status = param["status"],
                    id = id.ToString(),
                    transact_date = "",
                    lama_tumpuk = "",
                    created_date = param["created_date"],
                    tgl_mulai = param["tgl_mulai"]
                };
            }
            else if (page == "Container")
            {
                notification_data = new
                {
                    page = page,
                    kd_cabang = param["kd_cabang"],
                    kd_cabang_induk = "",
                    kd_regional = param["kd_regional"],
                    kd_terminal = param["kd_terminal"],
                    kd_agen = "",
                    nama_kapal = "",
                    pelanggan = param["pelanggan"],
                    unique = param["container_no"],
                    status = param["status"],
                    id = id.ToString(),
                    transact_date = param["transact_date"],
                    lama_tumpuk = param["lama_tumpuk"],
                    created_date = "",
                    tgl_mulai = ""
                };
            }


            string notif_destination = null;
            if(type == "GLOBAL")
            {
                notif_destination = "/topics/IBS-Pelindo-User";
            }
            else if (type == "SPECIFIC")
            {
                notif_destination = "/topics/IBS-MB-" + param["kd_cabang"] + "-" + param["kd_agen"];
                //notif_destination = "/topics/IBS-MB-02-99998";
            }

            try
            {
                var applicationID = "AAAAid5TXqQ:APA91bFhL2Rd-MQl8dOZ-Zbgq9ZAuFwFYE4mclpUeenvWYAE7Xq6zqQpmPIpgrzGT7vNb7eCuhx7CoEGvH2-LhIFDrQaJLhQefIOeNp6_gnvcmxw4ahAg6TbIf-wHVpO_bv59_sx4cS-";
                //var senderId = "1051215641905";
                //var applicationID = "AAAAid5TXqQ:APA91bFhL2Rd-MQl8dOZ-Zbgq9ZAuFwFYE4mclpUeenvWYAE7Xq6zqQpmPIpgrzGT7vNb7eCuhx7CoEGvH2-LhIFDrQaJLhQefIOeNp6_gnvcmxw4ahAg6TbIf-wHVpO_bv59_sx4cS-";
                //string[] deviceId = new string[] { "dSk4v9BLYEc:APA91bEEvvPPwZfsT4Uuvpqat8Zd6VUKbARrVV0KjI23jZP_lHLPTFG1UAUHxmjxHcU3onv1eNuN4U1kIVv0wnZUDDSY8ax6g9pYnZdnbwz1X0gCvDmKK80jTV2G1wMeUxKyUDHX9caW" };
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Proxy = null;
                tRequest.Timeout = 1000 * 60 * 5;
                var data = new
                {
                    priority = "high",
                    //to = "/topics/" + generateId,
                    to = notif_destination,
                    //registration_ids = deviceId,
                    notification = new
                    {
                        body = param["message"],
                        title = param["title"]
                    },
                    data = notification_data
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                //tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                str = sResponseFromServer;
                                res = "Notifikasi berhasil dikirim pada " + DateTime.Now.ToString("dd MMMM yyyy HH:mm");
                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                str = ex.Message;
                res = "Notifikasi gagal dikirim pada " + DateTime.Now.ToString("dd MMMM yyyy HH:mm");
            }

            return res;
        }

        public static int checkNotification(string message, string status, string kd_agen, string is_read, string title)
        {
            int result = 0;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    var sql = "SELECT * FROM T_MAGIC_NOTIFICATION WHERE MESSAGE='" + message + "' AND STATUS='" + status + "' AND KD_AGEN='" + kd_agen + "' AND TITLE='" + title + "' AND IS_READ='" + is_read + "'";

                    var execute = connection.Query(sql);

                    result = execute.ToList().Count();
                }
                catch (Exception)
                {
                    result = 0;
                }
            }

            return result;
        }

        public static string insertNotification(string message, string status, string kd_agen, string data, string title, int is_read, int id)
        {

            string result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {

                    int notifcount = Notifications.checkNotification(message, status, kd_agen, "0", title);

                    if (notifcount == 0)
                    {
                        var sql = "INSERT INTO T_MAGIC_NOTIFICATION VALUES('" + message + "', '" + status + "', '" + kd_agen + "', '" + data + "', '" + title + "', '" + is_read + "', '" + id + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        var execute = connection.Execute(sql);

                        if(execute == 1)
                        {
                            result = "Success";
                        }
                    }
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
