using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Model.Notification
{
    class NotificationModel
    {
    }

    class ParamNotification
    {
        public string id { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string kd_agen { get; set; }
        public string data { get; set; }
        public string title { get; set; }
        public bool is_read { set; get; }
    }
}
