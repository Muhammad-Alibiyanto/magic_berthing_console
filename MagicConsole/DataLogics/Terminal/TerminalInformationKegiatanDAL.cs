using Dapper;
using MagicConsole.DataLogics.Notification;
using MagicConsole.Model.Terminal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MagicConsole.DataLogics.Terminal
{
    class TerminalInformationKegiatanDAL
    {
        public static List<TerminalAvailable> getWorkingBoat()
        {

            List<TerminalAvailable> result = null;


            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string sql = "SELECT * FROM(" +
                       "SELECT * FROM (SELECT A.*, B.REGIONAL_NAMA NAMA_REGIONAL, " +
                       "(CASE WHEN A.TGL_MULAI IS NULL AND A.TGL_SELESAI IS NULL THEN 'RENCANA' " +
                       "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NULL THEN 'SANDAR' " +
                       "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NOT NULL THEN 'HISTORY' END" +
                       ") STATUS " +
                       "FROM VW_MAGIC_TRMNL_INFO_ALL A, APP_REGIONAL B WHERE A.KD_REGIONAL=B.ID AND B.PARENT_ID IS NULL AND B.ID NOT IN (12300000,20300001) )" +
                   ") WHERE STATUS = 'SANDAR'";


                    result = connection.Query<TerminalAvailable>(sql).ToList();
                }
                catch(Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public static string getBoatTerminalDetail()
        {
            string result = null;
            List<TerminalAvailable> temp = new List<TerminalAvailable>();

            foreach (var terminal in getWorkingBoat())
            {
                using (IDbConnection connection = Extension.GetConnection(1))
                {
                    try
                    {
                        string sql = "SELECT * FROM(" +
                            "SELECT * FROM (SELECT A.*, B.REGIONAL_NAMA NAMA_REGIONAL, " +
                            "(CASE WHEN A.TGL_MULAI IS NULL AND A.TGL_SELESAI IS NULL THEN 'RENCANA' " +
                            "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NULL THEN 'SANDAR' " +
                            "WHEN A.TGL_MULAI IS NOT NULL AND A.TGL_SELESAI IS NOT NULL THEN 'HISTORY' END" +
                            ") STATUS " +
                            "FROM VW_MAGIC_TRMNL_INFO_ALL A, APP_REGIONAL B WHERE A.KD_REGIONAL=B.ID AND B.PARENT_ID IS NULL AND B.ID NOT IN (12300000,20300001) ) WHERE NO_PPK_JASA='" + terminal.no_ppk_jasa + "' AND KEGIATAN IS NOT NULL AND TIPE IS NOT NULL" +
                        ")";

                        var data = connection.Query<TerminalAvailable>(sql).ToList();
                        foreach (var item in data)
                        {
                            temp.Add(new TerminalAvailable()
                            {
                                kd_cabang_induk = item.kd_cabang_induk,
                                kd_cabang = item.kd_cabang,
                                kd_terminal = item.kd_terminal,
                                kawasan = item.kawasan,
                                no_ppk1 = item.no_ppk1,
                                no_ppk_jasa = item.no_ppk_jasa,
                                nama_kapal = item.nama_kapal,
                                nama_lokasi = item.nama_lokasi,
                                nama_agen = item.nama_agen,
                                kegiatan = item.kegiatan,
                                jenis_kapal = item.jenis_kapal,
                                tgl_mulai_ptp = item.tgl_mulai_ptp,
                                tgl_selesai_ptp = item.tgl_selesai_ptp,
                                tgl_mulai = item.tgl_mulai,
                                tgl_selesai = item.tgl_selesai,
                                created = item.created,
                                start_work = item.start_work,
                                end_work = item.end_work,
                                tipe = item.tipe,
                                kd_regional = item.kd_regional,
                                kode_kapal = item.kode_kapal,
                                nama_pelabuhan_asal = item.nama_pelabuhan_asal,
                                nama_pelabuhan_tujuan = item.nama_pelabuhan_tujuan,
                                gt_kapal = item.gt_kapal,
                                loa = item.loa,
                                nama_regional = item.nama_regional,
                                kade_awal_ptp = item.kade_awal_ptp,
                                kade_akhir_ptp = item.kade_akhir_ptp,
                                kade_akhir = item.kade_akhir,
                                kade_awal = item.kade_awal,
                                status = item.status,
                                kode_agen = item.kode_agen,
                                status_nota = item.status_nota,
                                jumlah = item.jumlah,
                                jmlh_plan = item.jmlh_plan,
                                jml_plan_bongkar = item.jml_plan_bongkar,
                                jml_plan_muat = item.jml_plan_muat,
                                jml_real_bongkar = item.jml_real_bongkar,
                                jml_real_muat = item.jml_real_muat
                            });
                        }

                    }
                    catch (Exception)
                    {
                        temp = null;
                    }
                }
            }

            foreach(var sendNotifData in temp)
            {
                result = getBoatActivity(sendNotifData);
            }

            return result;
        }

        public static string getBoatActivity(TerminalAvailable parameter)
        {
            string result = null;

            if(parameter.tipe == "CARGO")
            {
                using (IDbConnection connection = Extension.GetConnection(0))
                {
                    try
                    {
                        string sql = "SELECT A.*, JMLH_PLAN, JMLH_TRUCK_PLAN FROM (SELECT KD_CABANG, KD_TERMINAL, (CASE WHEN KAWASAN = 'NILAM' OR KAWASAN = 'MIRAH' THEN 'NILAMMIRAH' ELSE KAWASAN END) NAMA_TERMINAL, NO_PMH_BM, NAMA_KAPAL, KEGIATAN, KD_BARANG, NAMA_BARANG, NAMA_DERMAGA, SUM(JUMLAH) JUMLAH, KD_SATUAN, SUM(JML_TRUCK) JML_TRUK, NM_PBM" +
                              " FROM(SELECT KD_CABANG, KD_TERMINAL, KAWASAN, NO_PMH_BM, NAMA_KAPAL, NM_PBM, KD_BARANG, MBRG_NAMA NAMA_BARANG, SHIFT, HARI, KEGIATAN, NAMA_DERMAGA, ALAT1, ALAT2, ALAT3, ALAT4, JUMLAH, KD_SATUAN, NVL(JML_TRUCK, 0) JML_TRUCK, JAM_MULAI, JAM_SELESAI, TOSGC.func_startwork_info(NO_PMH_BM, KD_CABANG, KEGIATAN) START_WORK, TOSGC.func_endkegiatan(kd_cabang, kd_terminal, no_pmh_bm, kegiatan) END_WORK, KD_REGIONAL FROM(SELECT A.*, B.NO_PMH_BM, E.PARAM_6 KAWASAN, B.NM_PBM, B.KEGIATAN, C.MBRG_NAMA, D.NAMA_KAPAL, TOSGC.func_getdermaga(A.KD_CABANG, A.KD_DERMAGA) NAMA_DERMAGA, E.KD_REGIONAL FROM TOSGC.TOSGCT_TALLY A, (SELECT B.*, C.NM_PBM FROM TOSGC.tosgct_pntp b, TOSGC.V_LIST_PBM_SAPUJAGAT C WHERE STATUS <> 'BATAL' AND B.NO_PMH_BM = C.NO_PMH_BM AND B.PBM = C.PBM AND B.NO_SPK = C.NO_SPK) B, TOSGC.TOSGCM_BARANG C, (SELECT TPPKB1_NOMOR, TPPKB1_NAMA_KAPAL NAMA_KAPAL FROM TOSGC.V_PPKB1_PMH_KAPAL_ALL) D, VASA.MASTER_PARAMETER_POCC E WHERE A.NO_SPK = B.NO_SPK AND A.KD_BARANG = C.MBRG_KODE AND A.KD_CABANG = E.PARAM_1 AND A.KD_TERMINAL = E.PARAM_2 AND E.PARAMETER_ID = 'MASTER_TERMINAL' AND B.NO_PMH_BM = D.TPPKB1_NOMOR) WHERE NO_PMH_BM = '" + parameter.no_ppk1 + "' AND KEGIATAN = '" + parameter.kegiatan + "'" +
                              " ORDER BY KD_CABANG, KD_TERMINAL ASC) GROUP BY KAWASAN, NO_PMH_BM, NAMA_KAPAL, KD_BARANG, NAMA_BARANG, NAMA_DERMAGA, KD_SATUAN, KEGIATAN, KD_CABANG, KD_TERMINAL, NM_PBM) A, (SELECT A.KD_CABANG, A.KD_TERMINAL, A.NO_PMH_BM, A.KEGIATAN, B.KD_BARANG, C.MBRG_NAMA, SUM(B.JML_PLAN) JMLH_PLAN, B.KD_SATUAN, SUM(B.JML_TRUK) JMLH_TRUCK_PLAN FROM TOSGC.TOSGCT_PNTP A, TOSGC.TOSGCT_OP_D B, TOSGC.TOSGCM_BARANG C" +
                              " WHERE A.STATUS <> 'BATAL' AND A.NO_OP = B.NO_OP AND B.KD_BARANG = C.MBRG_KODE(+) GROUP BY A.KD_CABANG, A.KD_TERMINAL, A.NO_PMH_BM, A.KEGIATAN, B.KD_BARANG, C.MBRG_NAMA, B.KD_SATUAN) B WHERE A.NO_PMH_BM = B.NO_PMH_BM AND A.KEGIATAN = B.KEGIATAN AND A.NAMA_BARANG = B.MBRG_NAMA";

                        var data = connection.Query<TerminalAvailable>(sql).ToList();

                        if (data.Count > 0)
                        {
                            foreach (var item in data)
                            {
                                if (item.jumlah != null && item.jumlah != "")
                                {
                                    var message = "TOTAL " + parameter.kegiatan + " KAPAL " + item.nama_kapal + " SAMPAI JAM " + DateTime.Now.ToString("HH:mm") + " MENCAPAI " + item.jumlah + " TON/M3 DARI RENCANA " + parameter.kegiatan + "  " + item.jmlh_plan + " TON/M3";

                                    Dictionary<String, String> param = new Dictionary<String, String>();
                                    param.Add("kd_cabang", parameter.kd_cabang); param.Add("kd_cabang_induk", parameter.kd_cabang_induk); param.Add("kd_regional", parameter.kd_regional);
                                    param.Add("regional", parameter.nama_regional); param.Add("terminal", parameter.kawasan); param.Add("dermaga", parameter.nama_lokasi); param.Add("pelabuhan_asal", parameter.nama_pelabuhan_asal); param.Add("pelabuhan_tujuan", parameter.nama_pelabuhan_tujuan); param.Add("no_ppk1", parameter.no_ppk1);
                                    param.Add("kd_terminal", parameter.kd_terminal); param.Add("no_ppk_jasa", parameter.no_ppk_jasa); param.Add("message", message); param.Add("nama_kapal", parameter.nama_kapal); param.Add("nama_agen", parameter.nama_agen);
                                    param.Add("status", "BONGKAR/MUAT"); param.Add("kd_agen", parameter.kode_agen); param.Add("title", "Terminal Information - " + parameter.kawasan + "/" + parameter.nama_lokasi);

                                    string notif_data = JsonSerializer.Serialize(param);
                                    Random id = new Random();
                                    int notif_id = id.Next(10000, 99999);
                                    string insertNotification = Notifications.insertNotification(message, "RENCANA", "99998", notif_data, "Terminal Information", 0, notif_id);

                                    var res = Notifications.sendNotification("SPECIFIC", "Terminal", param, notif_id);

                                    result = res + " (BONGKAR/MUAT TERMINAL INFORMATION)";
                                }
                                else
                                {
                                    result = "TIDAK ADA NOTIFIKASI DIKIRIM (BONGKAR/MUAT TERMINAL INFORMATION)";
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {
                        result = "TIDAK ADA NOTIFIKASI DIKIRIM (BONGKAR/MUAT TERMINAL INFORMATION)";
                    }
                }
            }
            else
            {
                using (IDbConnection connection = Extension.GetConnection(1))
                {
                    try
                    {
                        string sql = "SELECT* FROM VW_MAGIC_TRMNL_INFO_CONTAINER WHERE NO_PMH_BM='" + parameter.no_ppk1 + "' AND KEGIATAN='" + parameter.kegiatan + "'";
                        var data = connection.Query<TerminalAvailable>(sql).ToList();

                        if(data.Count > 0)
                        {
                            foreach (var item in data)
                            {
                                if (parameter.kegiatan == "BONGKAR")
                                {
                                    if (item.jml_plan_bongkar != null && item.jml_plan_bongkar != "" && item.jml_real_bongkar != null && item.jml_real_bongkar != "")
                                    {
                                        var message = "TOTAL " + parameter.kegiatan + " KAPAL " + item.nama_kapal + " SAMPAI JAM " + DateTime.Now.ToString("HH:mm") + " MENCAPAI " + item.jml_real_bongkar + " TON/M3 DARI RENCANA " + parameter.kegiatan + " " + item.jml_plan_bongkar + " TON/M3";

                                        Dictionary<String, String> param = new Dictionary<String, String>();
                                        param.Add("kd_cabang", parameter.kd_cabang); param.Add("kd_cabang_induk", parameter.kd_cabang_induk); param.Add("kd_regional", parameter.kd_regional);
                                        param.Add("regional", parameter.nama_regional); param.Add("terminal", parameter.kawasan); param.Add("dermaga", parameter.nama_lokasi); param.Add("pelabuhan_asal", parameter.nama_pelabuhan_asal); param.Add("pelabuhan_tujuan", parameter.nama_pelabuhan_tujuan); param.Add("no_ppk1", parameter.no_ppk1);
                                        param.Add("kd_terminal", parameter.kd_terminal); param.Add("no_ppk_jasa", parameter.no_ppk_jasa); param.Add("message", message); param.Add("nama_kapal", parameter.nama_kapal); param.Add("nama_agen", parameter.nama_agen);
                                        param.Add("status", "BONGKAR/MUAT"); param.Add("kd_agen", parameter.kode_agen); param.Add("title", "Terminal Information - " + parameter.kawasan + "/" + parameter.nama_lokasi);

                                        string notif_data = JsonSerializer.Serialize(param);
                                        Random id = new Random();
                                        int notif_id = id.Next(10000, 99999);
                                        string insertNotification = Notifications.insertNotification(message, "RENCANA", "99998", notif_data, "Terminal Information", 0, notif_id);

                                        var res = Notifications.sendNotification("SPECIFIC", "Terminal", param, notif_id);

                                        result = res + " (BONGKAR/MUAT TERMINAL INFORMATION)";
                                    }
                                    else
                                    {
                                        result = "TIDAK ADA NOTIFIKASI DIKIRIM (BONGKAR/MUAT TERMINAL INFORMATION)";
                                    }
                                }
                                if (parameter.kegiatan == "MUAT")
                                {
                                    if (item.jml_plan_muat != null && item.jml_plan_muat != "" && item.jml_real_muat != null && item.jml_real_muat != "")
                                    {
                                        var message = "TOTAL " + parameter.kegiatan + " KAPAL " + item.nama_kapal + " SAMPAI JAM " + DateTime.Now.ToString("HH:mm") + " MENCAPAI " + item.jml_real_muat + " TON/M3 DARI RENCANA " + parameter.kegiatan + " " + item.jml_plan_muat + " TON/M3";

                                        Dictionary<String, String> param = new Dictionary<String, String>();
                                        param.Add("kd_cabang", parameter.kd_cabang); param.Add("kd_cabang_induk", parameter.kd_cabang_induk); param.Add("kd_regional", parameter.kd_regional);
                                        param.Add("regional", parameter.nama_regional); param.Add("terminal", parameter.kawasan); param.Add("dermaga", parameter.nama_lokasi); param.Add("pelabuhan_asal", parameter.nama_pelabuhan_asal); param.Add("pelabuhan_tujuan", parameter.nama_pelabuhan_tujuan); param.Add("no_ppk1", parameter.no_ppk1);
                                        param.Add("kd_terminal", parameter.kd_terminal); param.Add("no_ppk_jasa", parameter.no_ppk_jasa); param.Add("message", message); param.Add("nama_kapal", parameter.nama_kapal); param.Add("nama_agen", parameter.nama_agen);
                                        param.Add("status", "BONGKAR/MUAT"); param.Add("kd_agen", parameter.kode_agen); param.Add("title", "Terminal Information - " + parameter.kawasan + "/" + parameter.nama_lokasi);

                                        string notif_data = JsonSerializer.Serialize(param);
                                        Random id = new Random();
                                        int notif_id = id.Next(10000, 99999);
                                        string insertNotification = Notifications.insertNotification(message, "RENCANA", "99998", notif_data, "Terminal Information", 0, notif_id);

                                        var res = Notifications.sendNotification("SPECIFIC", "Terminal", param, notif_id);

                                        result = res + " (BONGKAR/MUAT TERMINAL INFORMATION)";
                                    }
                                    else
                                    {
                                        result = "TIDAK ADA NOTIFIKASI DIKIRIM (BONGKAR/MUAT TERMINAL INFORMATION)";
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {
                        result = "TIDAK ADA NOTIFIKASI DIKIRIM (BONGKAR/MUAT TERMINAL INFORMATION)";
                    }
                }
            }

            return result;
        }
    }
}