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
    public partial class FrmHastaDetay : Form
    {

        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;

        sqlbaglantisi bgl = new sqlbaglantisi();

        void bos_randevu_listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM randevular WHERE randevu_brans = '" + CmbBrans.Text + "'" + "and randevu_doktor='" + CmbDoktor.Text + "' and randevu_durum = 0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void hasta_randevusu_listele()//Randevu geçmişi sorgulama
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM randevular WHERE hasta_tc =" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //Hasta tc ve ad soyadı form ekranına getirme
            LblTC.Text = tc ;
            SqlCommand komut = new SqlCommand("SELECT hasta_ad , hasta_soyad FROM hastalar WHERE  hasta_tc = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();



            //Randevu geçmişi sorgulama
            hasta_randevusu_listele();


            //Boş randevuları sorgulama
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM randevular", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;





            //Bransları Çekme
            SqlCommand komut2 = new SqlCommand("SELECT brans_ad FROM branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


            



        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktorları Çekme
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT doktor_ad , doktor_soyad FROM doktorlar WHERE doktor_brans = @p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            bos_randevu_listele();
        }

        private void LnkBilgiGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiGuncelle frm = new FrmBilgiGuncelle();
            frm.tc = tc;
            frm.Show();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            TxtRandevuID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            CmbBrans.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
            CmbDoktor.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE randevular SET hasta_tc = @p1 ,randevu_durum = @p2 ,hasta_sikayet = @p3 WHERE randevu_id = @p4", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",tc);
            komut.Parameters.AddWithValue("@p2", 1);
            komut.Parameters.AddWithValue("@p3",RtxtSikayet.Text);
            komut.Parameters.AddWithValue("@p4", TxtRandevuID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Randevu alımınız gerçekleşmiştir","İşlem Başarılı" , MessageBoxButtons.OK ,MessageBoxIcon.Information);
            bgl.baglanti().Close();
            bos_randevu_listele();
            hasta_randevusu_listele();
        }
    }
}
