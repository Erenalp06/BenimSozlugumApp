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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + Application.StartupPath + "\\sozlukvt_1.2.accdb");
        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "BenimSözlüğüm Çeviri";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.CancelButton = button3;
            label2.Text = Form1.ad + " " + Form1.soyad;
            bunifuMaterialTextbox1.Text.ToLower();
            bunifuMaterialTextbox1.characterCasing = CharacterCasing.Lower;
            listBox1.Items.Clear();
            
        }
        bool kontrol = false;
        private void button2_Click(object sender, EventArgs e)
        {
            string sifre = bunifuMaterialTextbox1.Text;
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;

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
                bunifuMaterialTextbox1.Text = sifre;
                MessageBox.Show("Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!", "BenimSözlüğümUyarıMesajı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            baglantim.Open();
            OleDbCommand selectsorgusu = new OleDbCommand("select * from sozluk" ,baglantim);
            OleDbDataReader oku = selectsorgusu.ExecuteReader();

            while(oku.Read())
            {

                if(oku["ingilizce"].ToString() == sifre)
                {
                    
                    label4.Text = oku["turkce"].ToString();
                    listBox1.Items.Add(oku["ingilizce"].ToString() + "=" + oku["turkce"].ToString());
                    kontrol = true;
                    break;
                }
                else
                {
                    kontrol = false;
                }

            }
            baglantim.Close();

            if(kontrol == false)
            {
                MessageBox.Show("Aradığınız Kelime Veritabanında Bulunamadı", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Text = "HATA!";
            }

            

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        bool kontrol1 = false;
        private void button1_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand selectsorgusu = new OleDbCommand("select *from sozluk", baglantim);
            OleDbDataReader oku = selectsorgusu.ExecuteReader();

            while(oku.Read())
            {
                if(oku["turkce"].ToString() == bunifuMaterialTextbox1.Text)
                {
                    label4.Text = oku["ingilizce"].ToString();
                    listBox1.Items.Add(oku["turkce"].ToString() + "=" + oku["ingilizce"].ToString());
                    kontrol1 = true;
                    break;
                }
                else
                {
                    kontrol1 = false;
                }
            }
            baglantim.Close();

            if(kontrol1 == false)
            {
                MessageBox.Show("Aradığınız Kelime Veritabanında Bulunamadı", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Text = "HATA!";
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
