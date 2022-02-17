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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit frm = new FrmHastaKayit();
            frm.Show();
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM hastalar WHERE hasta_tc = @p1 AND hasta_sifre = @p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTcNo.Text);
            komut.Parameters.AddWithValue("@p2",TxtParola.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmHastaDetay frm = new FrmHastaDetay();
                frm.tc = MskTcNo.Text;     //hasta detay formundaki public tc string değişlkenine kullanıcı tc sini atamamı sağladı
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Parolanız ya da TC'niz yanlış","Giriş Başarısız",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
