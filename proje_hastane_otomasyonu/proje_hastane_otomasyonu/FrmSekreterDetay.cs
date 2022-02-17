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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            //Sekreter tc ve ad soyad getirme
            LblTC.Text = tc;
            SqlCommand komut = new SqlCommand("SELECT sekreter_ad_soyad FROM sekreterler WHERE sekreter_tc = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları Datagride aktarma
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * from branslar ",bgl.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            DgwBranslar.DataSource = dt1;
            

            //Doktorları Datagride aktarmak
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT (doktor_ad + ' ' + doktor_soyad) as 'Doktorlar' , doktor_brans FROM doktorlar",bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            DgwDoktor.DataSource = dt2;


            //combobox'a veri ekleme
            SqlCommand komut2 = new SqlCommand("SELECT brans_Ad FROM branslar ",bgl.baglanti());
            SqlDataReader dr1 = komut2.ExecuteReader();
           while(dr1.Read())
            {
                CmbBrans.Items.Add(dr1[0]);
            }
            bgl.baglanti().Close();
        }






        private void BtnBranşP_Click(object sender, EventArgs e)
        {
            FrmBransPanel frm = new FrmBransPanel();
            frm.Show();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut_kaydet = new SqlCommand("INSERT INTO randevular (randevu_tarih,randevu_saat,randevu_brans,randevu_doktor) VALUES (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komut_kaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            komut_kaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            komut_kaydet.Parameters.AddWithValue("@r3",CmbBrans.Text);
            komut_kaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komut_kaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","İşlem Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Text = "";
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT (doktor_ad + ' ' + doktor_soyad) FROM doktorlar WHERE doktor_brans = @p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO duyurular (duyuru_metin) VALUES (@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",RtxtDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
            RtxtDuyuru.Clear();
        }

        private void BtnDoktorP_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.Show();
        }

        private void BtnRandevuL_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frm = new FrmRandevuListesi();
            frm.Show();
        }

        private void BtnDuyuruListele_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }
    }
}
