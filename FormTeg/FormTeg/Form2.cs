using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTeg
{
    public partial class Form2 : Form
    {
   
        public Form2()
        {
            InitializeComponent();
            listele();
            comboBox1.Items.Add("Android Developer");
            comboBox1.Items.Add("Web Developer");
            comboBox1.Items.Add("Data Science");

            comboBox2.Items.Add("Akıllı Asistan");
            comboBox2.Items.Add("Hastane Otomasyon Sistemi");
            comboBox2.Items.Add("E-Ticaret Sitesi");
            comboBox2.Items.Add("Duygu Analizi");

            comboBox3.Items.Add("1");
            comboBox3.Items.Add("2");
            comboBox3.Items.Add("3");
            comboBox3.Items.Add("4");
            comboBox3.Items.Add("5");
            comboBox3.Items.Add("6");

        }
        int sicilNo = 0;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1U7J60F;Initial Catalog=Personel;Integrated Security=True");
        private void listele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Personel.sicilNo, Personel.personelAdi, Personel.personelSoyadi, Proje.projeAdi, " +
                "Personel.odaNo, Bolum.bolumAdi " + " From Personel, Proje, Bolum Where Personel.projeNo = Proje.projeNo AND Personel.bolumNo = Bolum.bolumNo", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == null || textBox2.Text==null || textBox3.Text == null || comboBox1.Text == null
                ||  comboBox2.Text == null || comboBox3.Text== null)
            {
                MessageBox.Show("Lütfen tüm alanların doldurulduğundan emin olunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                baglanti.Open();
                SqlCommand komutEkle = new SqlCommand("insert into Personel(personelAdi, personelSoyadi, sicilNo, bolumNo, projeNo, odaNo) values (@p1, @p2, @p3, @p4, @p5, @p6)", baglanti);
                int a;
                a = comboBox1.SelectedIndex + 1;
                int b;
                b = comboBox2.SelectedIndex + 1;
                int c;
                c = comboBox3.SelectedIndex + 1;

                komutEkle.Parameters.AddWithValue("@p1", textBox2.Text);
                komutEkle.Parameters.AddWithValue("@p2", textBox3.Text);
                komutEkle.Parameters.AddWithValue("@p3", Convert.ToInt32(textBox1.Text));
                komutEkle.Parameters.AddWithValue("@p4", a);
                komutEkle.Parameters.AddWithValue("@p5", b);
                komutEkle.Parameters.AddWithValue("@p6", c);
                komutEkle.ExecuteNonQuery();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();

                baglanti.Close();
                listele();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From Personel Where sicilNo = (" + sicilNo + ")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("Update Personel Set personelAdi = @p1, personelSoyadi = @p2, projeNo = @p3, odaNo = @p4, bolumNo = @p5" + "Where sicilNo = " + sicilNo + "", baglanti);
            komutGuncelle.Parameters.AddWithValue("@p1", textBox1.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", textBox2.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", comboBox2.SelectedIndex + 1);
            komutGuncelle.Parameters.AddWithValue("@p4", comboBox3.SelectedIndex + 1);
            komutGuncelle.Parameters.AddWithValue("@p5", comboBox1.SelectedIndex + 1);

            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Personel Bilginiz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select Personel.sicilNo, Personel.personelAdi, Personel.personelSoyadi, Proje.projeAdi, Personel.odaNo, Bolum.bolumAdi " + "From Personel, Proje, Bolum Where Personel.projeNo = Proje.projeNo AND Personel.bolumNo = Bolum.bolumNo", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

      
       private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sicilNo = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = sicilNo.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


      

    }
}
