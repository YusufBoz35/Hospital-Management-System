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
    public partial class FrmBilgiGuncelle : Form
    {
        public FrmBilgiGuncelle()
        {
            InitializeComponent();
        }


        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmBilgiGuncelle_Load(object sender, EventArgs e)
        {
            MskTcNo.Text = tc;
            SqlCommand komut = new SqlCommand("SELECT * FROM hastalar WHERE hasta_tc = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTcNo.Text = dr[3].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
                MskTel.Text = dr[4].ToString();
                TxtParola.Text = dr[5].ToString();
                
            }

             bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("UPDATE hastalar SET hasta_ad  = @p1 , hasta_soyad = @p2 , hasta_tc = @p3 , hasta_telefon = @p4 , hasta_sifre = @p5 , hasta_cinsiyet = @p6 WHERE hasta_tc  = @p7 " , bgl.baglanti()) ;
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTcNo.Text);
            komut2.Parameters.AddWithValue("@p4", MskTel.Text);
            komut2.Parameters.AddWithValue("@p5", TxtParola.Text);
            komut2.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p7", tc);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
