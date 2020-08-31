using System;
using System.Collections.Generic;
using System.Text;

namespace MagicConsole.Model.Warehouse
{
    class WarehouseInformationModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<WarehouseAvailable> data { get; set; }
    }

    public class WarehouseAvailable
    {
        public string nama_terminal { get; set; }
        public string nama_vak { get; set; }
        public string nama_barang { get; set; }
        public string pelanggan { get; set; }
        public string jumlah_real { get; set; }
        public string tgl_mulai { get; set; }
        public string tipe_penumpukan { get; set; }
        public string occupied { get; set; }
        public string kd_region { get; set; }
        public string created_date { get; set; }
        public string lama_tumpuk { get; set; }
        public string nama_kapal { get; set; }
        public string kd_cabang { get; set; }
        public string kd_terminal { get; set; }
        public string kd_gudlap_d { get; set; }
        public string mglap_nama { get; set; }
        public string last_updated { get; set; }
        public string nama_regional { get; set; }
    }
}
