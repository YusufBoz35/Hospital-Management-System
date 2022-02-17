using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace proje_hastane_otomasyonu
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            //doktor tc ve ad_soyad getirme
            LblTC.Text = tc;
            SqlCommand komut = new SqlCommand("SELECT (doktor_ad + ' ' + doktor_soyad) FROM doktorlar WHERE doktor_tc = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }


            //randevuları listele
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM randevular WHERE randevu_doktor ='"+LblAdSoyad.Text +"'",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiGuncelle frm = new FrmDoktorBilgiGuncelle();
            frm.doktor_tc = tc;
            frm.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string randevu_id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            SqlCommand komut = new SqlCommand("SELECT hasta_sikayet FROM randevular WHERE randevu_id = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",randevu_id);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                RtxtRandevu.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
