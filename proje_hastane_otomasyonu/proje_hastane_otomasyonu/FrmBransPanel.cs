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
    public partial class FrmBransPanel : Form
    {
        public FrmBransPanel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM branslar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            TxtBransAd.Text = "";
            TxtBransID.Text = "";
        }


        private void FrmBransPanel_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut_ekle = new SqlCommand("INSERT INTO branslar (brans_ad) VALUES (@p1)",bgl.baglanti());
            komut_ekle.Parameters.AddWithValue("@p1", TxtBransAd.Text);
            komut_ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni brans sisteme eklenedi","İşlem Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            listele();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut_sil = new SqlCommand("DELETE FROM branslar WHERE brans_id = @p1",bgl.baglanti());
            komut_sil.Parameters.AddWithValue("@p1", TxtBransID.Text);
            komut_sil.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            listele();
            MessageBox.Show("Seçilen brans sistemden silindi", "İşlem başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut_guncelle = new SqlCommand("UPDATE branslar SET brans_ad = @p1 WHERE brans_id = @p2", bgl.baglanti());
            komut_guncelle.Parameters.AddWithValue("@p1",TxtBransAd.Text);
            komut_guncelle.Parameters.AddWithValue("@p2", TxtBransID.Text);
            komut_guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            listele();
            MessageBox.Show("Seçilen brans güncellendi", "İşlem başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtBransID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }
    }
}
