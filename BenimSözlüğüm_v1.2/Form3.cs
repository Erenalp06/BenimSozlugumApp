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
using System.Text.RegularExpressions;

namespace BenimSözlüğüm_v1._2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + Application.StartupPath + "\\Kullanici_bilgisi.accdb");

        private void Form3_Load(object sender, EventArgs e)
        {
            label3.Text = "";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Text = "Kullanıcı Kayıt Ekranı";
            bunifuMaterialTextbox1.characterCasing = CharacterCasing.Upper;
            bunifuMaterialTextbox2.characterCasing = CharacterCasing.Upper;
            bunifuMaterialTextbox3.MaxLength = 8;
            bunifuMaterialTextbox4.MaxLength = 10;
            bunifuProgressBar1.MaximumValue = 100;
            bunifuProgressBar1.Value = 0;
        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void bunifuMaterialTextbox3_OnValueChanged(object sender, EventArgs e)
        {
            if (bunifuMaterialTextbox3.Text.Length != 8)
            {
                errorProvider1.SetError(bunifuMaterialTextbox3, "Kullanıcı adı 8 karakterden oluşmalı");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void bunifuMaterialTextbox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }
        int parola_skoru = 0;
        private void bunifuMaterialTextbox4_OnValueChanged(object sender, EventArgs e)
        {
            label3.Visible = true;
            string parola_Seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = bunifuMaterialTextbox4.Text;

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

            if(sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                bunifuMaterialTextbox4.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler ingilizce karakterlere dönüştürüldü!");
            }

            // 1 küçük harf 10 puan 2 tanesi 20 sonrası 20
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            // 1 büyük harf 10 puan 2 tanesi 20 sonrası yine 20
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            //1 rakam 10 puan 2 tanesi 20 puan gerisi 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            // 1 sembol 10 puan gerisi 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if(sifre.Length == 9)
            {
                parola_skoru += 10;
            }
            else if(sifre.Length == 10)
            {
                parola_skoru += 20;
            }

            if(kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
            {
                label3.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın !";

            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label3.Text = "";

            }

            if (parola_skoru >= 0 && parola_skoru < 40)
            {
                parola_Seviyesi = "Çok kötü";
                bunifuProgressBar1.ProgressColor = Color.Red;
            }
            else if(parola_skoru >= 40 && parola_skoru < 70)
            {
                parola_Seviyesi = "Kabul Edilemez";
                bunifuProgressBar1.ProgressColor = Color.Yellow;
            }
            else if (parola_skoru == 70 || parola_skoru == 80)
            {
                parola_Seviyesi = "Güçlü";
                bunifuProgressBar1.ProgressColor = Color.Green;
            }
            else if (parola_skoru == 90 || parola_skoru == 100)
            {
                parola_Seviyesi = " Çok Güçlü";
                bunifuProgressBar1.ProgressColor = Color.DarkGreen;
            }
            label1.Text = parola_skoru.ToString();
            label2.Text = parola_Seviyesi;
            bunifuProgressBar1.Value = parola_skoru;

        }
        private void temizle ()
        {
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox4.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            string yetki = "";
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgusu = new OleDbCommand("select * from kullanici where ad = '"+bunifuMaterialTextbox1.Text+"'", baglantim);
            OleDbDataReader okuma = selectsorgusu.ExecuteReader();

            while(okuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if(kayitkontrol == false)
            {
                //Ad kontrolü

                if(bunifuMaterialTextbox1.Text.Length < 2 || bunifuMaterialTextbox1.Text == "")
                {
                    label4.Visible = true;
                }
                else
                {
                    label4.Visible = false;
                }

                //soyad kontrolü

                if(bunifuMaterialTextbox2.Text.Length < 2 || bunifuMaterialTextbox1.Text == "")
                {
                    label5.Visible = true;
                }
                else
                {
                    label5.Visible = false;
                }

                // kullaniciadi kontrolü

                if(bunifuMaterialTextbox3.Text.Length != 8 || bunifuMaterialTextbox3.Text == "")
                {
                    label6.Visible = true;
                }
                else
                {
                    label6.Visible = false;
                }

                // Parola kontrolü

                if(bunifuMaterialTextbox4.Text == "" || parola_skoru < 70)
                {
                    label7.Visible = true;
                }
                else
                {
                    label7.Visible = false;
                }

                // raidobutton kontrolü

                

                if(bunifuMaterialTextbox1.Text != "" && bunifuMaterialTextbox2.Text != "" && bunifuMaterialTextbox3.Text.Length == 8 && bunifuMaterialTextbox3.Text != "" &&
                    bunifuMaterialTextbox4.Text != "" && parola_skoru >= 70 && (radioButton1.Checked == true || radioButton2.Checked == true)
                    )
                {
                    label8.Visible = false;
                    if(radioButton1.Checked == true)
                    {
                        yetki = "Yönetici";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }

                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklesorgusu = new OleDbCommand("insert into kullanici values ('"+bunifuMaterialTextbox1.Text+"', '"+bunifuMaterialTextbox2.Text+"', '"+bunifuMaterialTextbox3.Text+"', '"+bunifuMaterialTextbox4.Text+"', '"+yetki+"')", baglantim);
                        eklesorgusu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni Kullanıcı kaydı oluşturuldu!", "BenimSözlüğümBilgiMesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }

                }
                else
                {
                    label8.Visible = true;
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Girilem Ad ile zaten bir kullanıcı kaydı mevcut!", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fmr1 = new Form1();
            fmr1.Show();
            this.Hide();
        }
    }
}
