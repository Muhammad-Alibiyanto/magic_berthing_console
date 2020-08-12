using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Model.Pilot
{
    class PilotInformationModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<AvailablePilot> data { get; set; }
    }

    public class AvailablePilot
    {
        public string id_master_area { get; set; }
        public string nama { get; set; }
        public string call_sign { get; set; }
        public string kawasan { get; set; }
        public string nama_kapal { get; set; }
        public string tgl_work { get; set; }
        public string asal { get; set; }
        public string tujuan { get; set; }
        public string status { get; set; }
        public string kd_agen { get; set; }
        public string tgl_off { get; set; }
        public string from_mdmg_kode { get; set; }
        public string from_mdmg_nama { get; set; }
        public string to_mdmg_kode { get; set; }
        public string to_mdmg_nama { get; set; }
        public string kd_regional { get; set; }
        public string regional_nama { get; set; }
        public string nama_agen { get; set; }
        public string gerakan { get; set; }
        public string no_ppk1 { get; set; }
        public string urutan { get; set; }
    }
}
