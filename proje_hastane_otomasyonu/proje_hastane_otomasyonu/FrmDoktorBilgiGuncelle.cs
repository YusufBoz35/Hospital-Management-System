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
    public partial class FrmDoktorBilgiGuncelle : Form
    {
        public FrmDoktorBilgiGuncelle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string doktor_tc;

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE doktorlar SET doktor_ad = @p1,doktor_soyad = @p2 ,doktor_brans = @p3 , doktor_sifre = @p4 WHERE doktor_tc = @p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", TxtParola.Text);
            komut.Parameters.AddWithValue("@p5", MskTcNo.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void FrmDoktorBilgiGuncelle_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT brans_ad FROM branslar",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                CmbBrans.Items.Add(dr[0]);
            }


            SqlCommand komut1 = new SqlCommand("SELECT * FROM doktorlar WHERE doktor_tc = 11111111111",bgl.baglanti());
            //komut1.Parameters.AddWithValue("@p1", doktor_tc);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                TxtAd.Text = dr1[1].ToString();
                TxtSoyad.Text = dr1[2].ToString();
                CmbBrans.Text = dr1[3].ToString();
                MskTcNo.Text = dr1[4].ToString();
                TxtParola.Text = dr1[5].ToString();

            }
            bgl.baglanti().Close();


        }
    }
}
