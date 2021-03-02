using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BenimSözlüğüm_v1._2
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + Application.StartupPath+"\\sozlukvt_1.2.accdb");
        private void sözcükleri_göster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter listele = new OleDbDataAdapter("select ingilizce AS[İNGİLİZCE], turkce AS[TÜRKÇE] from sozluk", baglanti);
                DataSet tablo = new DataSet();
                listele.Fill(tablo);
                dataGridView1.DataSource = tablo.Tables[0];
                baglanti.Close();
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message, "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            this.Text = "BenimSözlüğüm Sözlük";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            label2.Text = Form1.ad + " " + Form1.soyad;
            sözcükleri_göster();
            textBox1.CharacterCasing = CharacterCasing.Lower;
            textBox2.CharacterCasing = CharacterCasing.Lower;
            textBox1.Height = textBox1.PreferredHeight;


        }
        bool durum = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                string sifre = textBox1.Text;

                string duzeltilmis_sifre = "";
                duzeltilmis_sifre = sifre;


                duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

                if (sifre != duzeltilmis_sifre)
                {
                    sifre = duzeltilmis_sifre;
                    textBox1.Text = sifre;
                    MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                try
                {
                    baglanti.Open();
                    OleDbCommand selectsorgusu = new OleDbCommand("select * from sozluk", baglanti);
                    OleDbDataReader oku = selectsorgusu.ExecuteReader();

                    while (oku.Read())
                    {
                        if (oku["ingilizce"].ToString() == textBox1.Text)
                        {
                            durum = true;
                            break;
                        }
                        else
                        {
                            durum = false;
                        }
                    }


                    if (durum == false)
                    {
                        OleDbCommand eklesorgusu = new OleDbCommand("insert into sozluk (ingilizce, turkce) values ('" + textBox1.Text + "', '" + textBox2.Text + "')", baglanti);
                        eklesorgusu.ExecuteNonQuery();
                        MessageBox.Show("Sözcük Veritabanına Başarıyla Eklendi.", "BenimSözlüğüm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        baglanti.Close();
                        sözcükleri_göster();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Eklemek İstediğiniz Sözcük Zaten Veritabanında Mevcut.", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Lüften Her iki kutucuğa da veri giriniz!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
            }


            


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            button5.Visible = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }
        bool durum1 = false;
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                


                try
                {

                    baglanti.Open();
                    OleDbCommand selectsorgusu = new OleDbCommand("select * from sozluk", baglanti);
                    OleDbDataReader oku = selectsorgusu.ExecuteReader();

                    while (oku.Read())
                    {
                        if (oku["ingilizce"].ToString() == textBox1.Text)
                        {
                            durum1 = true;
                            break;
                        }
                        else
                        {
                            durum1 = false;
                        }
                    }
                    if (durum1 == true)
                    {
                        OleDbCommand güncelle = new OleDbCommand("update sozluk set turkce ='" + textBox2.Text + "' where ingilizce = '" + textBox1.Text + "'", baglanti);
                        güncelle.ExecuteNonQuery();
                        baglanti.Close();
                        sözcükleri_göster();
                        MessageBox.Show("Sözcük Başarıyla Güncellendi", "BenimSözlüğüm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        button4.Visible = false;
                        button5.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Güncellemek İstediğiniz Sözcük Veritabanında Mevcut Değil.", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button4.Visible = false;
                        button5.Visible = false;
                        baglanti.Close();
                    }










                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Lüften Her iki kutucuğa da veri giriniz!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Visible = false;
                button5.Visible = false;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string sifre = textBox1.Text;

                string duzeltilmis_sifre = "";
                duzeltilmis_sifre = sifre;


                duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
                duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');



                if (sifre != duzeltilmis_sifre)
                {
                    sifre = duzeltilmis_sifre;
                    textBox1.Text = sifre;
                    MessageBox.Show("Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



                try
                {
                    baglanti.Open();
                    OleDbCommand selectsorgusu = new OleDbCommand("select * from sozluk", baglanti);
                    OleDbDataReader oku = selectsorgusu.ExecuteReader();

                    while (oku.Read())
                    {
                        if (oku["turkce"].ToString() == textBox2.Text)
                        {
                            durum1 = true;
                            break;
                        }
                        else
                        {
                            durum1 = false;
                        }
                    }
                    if (durum1 == true)
                    {
                        OleDbCommand güncelle = new OleDbCommand("update sozluk set ingilizce ='" + textBox1.Text + "' where turkce = '" + textBox2.Text + "'", baglanti);
                        güncelle.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Sözcük Başarıyla Güncellendi", "BenimSözlüğüm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sözcükleri_göster();
                        textBox1.Clear();
                        textBox2.Clear();
                        button4.Visible = false;
                        button5.Visible = false;

                    }
                    else
                    {
                        MessageBox.Show("Güncellemek İstediğiniz Sözcük Veritabanında Mevcut Değil.", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button4.Visible = false;
                        button5.Visible = false;
                        baglanti.Close();
                    }
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Lüften Her iki kutucuğa da veri giriniz!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Visible = false;
                button5.Visible = false;

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                try
                {
                    baglanti.Open();
                    OleDbCommand silsorgusu = new OleDbCommand("delete * from sozluk where ingilizce = '" + textBox1.Text + "'", baglanti);
                    silsorgusu.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Sözcük Veritabanından Başarıyla Silindi.", "BenimSözlüğüm", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    sözcükleri_göster();
                    textBox1.Clear();
                    textBox2.Clear();
                    
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Silmeye Çalıştığınız Sözcük Veritabanında Bulunamadı.", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                baglanti.Open();
                OleDbCommand select = new OleDbCommand("select *from sozluk where ingilizce like '"+textBox1.Text+"%'", baglanti);
                OleDbDataReader oku = select.ExecuteReader();

                while(oku.Read())
                {
                    listBox1.Items.Add(oku["ingilizce"].ToString() + ":" + oku["turkce"].ToString());
                }
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglanti.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                listBox1.Items.Clear();
                baglanti.Open();
                OleDbCommand select = new OleDbCommand("select *from sozluk where turkce like '"+textBox2.Text+"%'", baglanti);
                OleDbDataReader oku = select.ExecuteReader();

                while (oku.Read())
                {
                    listBox1.Items.Add(oku["turkce"].ToString() + ":" + oku["ingilizce"].ToString());
                }
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglanti.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
