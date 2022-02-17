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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM doktorlar WHERE doktor_tc = @p1 AND doktor_sifre = @p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTcNo.Text);
            komut.Parameters.AddWithValue("@p2",TxtParola.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.tc = MskTcNo.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Tc ya da şifreniz yanlış","İşlem Başarısız",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
