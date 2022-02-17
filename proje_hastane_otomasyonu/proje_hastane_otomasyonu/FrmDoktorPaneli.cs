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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        void temizle()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtSifre.Text = "";
            CmbBrans.Text = "";
            MskTC.Text = "";

        }

        void listele()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM doktorlar", bgl.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

        }




        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            listele();

            //combobox 'a  brans bilgilerini çek
            SqlCommand komut = new SqlCommand("SELECT brans_ad FROM branslar",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                CmbBrans.Items.Add(dr[0].ToString());
            }

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut_ekle = new SqlCommand("INSERT INTO doktorlar (doktor_ad  ,doktor_soyad  , doktor_brans , doktor_tc , doktor_sifre) VALUES (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
            komut_ekle.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut_ekle.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut_ekle.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komut_ekle.Parameters.AddWithValue("@p4",MskTC.Text);
            komut_ekle.Parameters.AddWithValue("@p5",TxtSifre.Text);
            komut_ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Doktor Sisteme Eklendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
            listele();
        }
        
        
        //datagrideview'dan verileri çekmek
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut_sil = new SqlCommand("DELETE FROM doktorlar WHERE doktor_tc = @p1",bgl.baglanti());
            komut_sil.Parameters.AddWithValue("@p1",MskTC.Text);
            komut_sil.ExecuteNonQuery();
            temizle();
            listele();
            MessageBox.Show("Seçili doktor sistemden silinmiştir", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut_guncelle = new SqlCommand("UPDATE doktorlar SET doktor_ad = @p1 ,doktor_soyad = @p2 , doktor_brans = @p3 , doktor_sifre = @p4 WHERE doktor_tc = @p5 ",bgl.baglanti());
            komut_guncelle.Parameters.AddWithValue("@p5",MskTC.Text);
            komut_guncelle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut_guncelle.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut_guncelle.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut_guncelle.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut_guncelle.ExecuteNonQuery();
            listele();
            MessageBox.Show("Seçili doktor bilgileri güncellenmiştir" , "İşlem Başarılı" , MessageBoxButtons.OK , MessageBoxIcon.Asterisk);
            bgl.baglanti().Close();
        }
    }
}
