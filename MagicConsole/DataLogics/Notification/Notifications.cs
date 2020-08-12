﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MagicConsole.Model.Terminal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicConsole.DataLogics.Notification
{
    class Notifications
    {
        public static string sendNotification(string type, string page, Dictionary<String, String> param)
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
                unique = ""
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
                    unique = param["no_ppk_jasa"]
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
                    unique = param["no_ppk1"]
                };
            }


            string notif_destination = null;
            if(type == "GLOBAL")
            {
                notif_destination = "/topics/IBS-Pelindo-User";
            }
            else if (type == "SPECIFIC")
            {
                //notif_destination = "/topics/IBS-KP-" + param["kd_cabang"] + "-" + param["kd_agen"];
                notif_destination = "/topics/IBS-KP-02-99998";
            }

            try
            {
                var applicationID = "AAAA9MFVvTE:APA91bEtnEGKbnqWn-OQQj80n2GUx2gFw1F_gJEuZJML8KyFJl7bPjd4Y3Ao-0FCQAPn2qXeqRRD8Yniv8WfehMI5lN6eSxqBiAphJ3TTy8aiaVrloae9vlQmoc8sqXhTQTkDm6cBiU9";
                var senderId = "1051215641905";
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
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
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
    }
}