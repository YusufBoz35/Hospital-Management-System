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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM sekreterler WHERE  sekreter_tc = @p1 AND sekreter_sifre = @p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" , MskTcNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtParola.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmSekreterDetay frm = new FrmSekreterDetay();
                frm.tc = MskTcNo.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC'niz  yada şifreniz hatalı","Giriş Başarısız",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        
    }
}
