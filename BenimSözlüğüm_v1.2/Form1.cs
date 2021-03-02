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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source="+ Application.StartupPath+"\\Kullanici_bilgisi.accdb");
        //formlar arası değişkenler
        public static string ad, soyad, yetki;

        int giris_hakkı = 3;
        bool durum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            this.Text = "Kullanıcı Giriş Ekranı";
            this.AcceptButton = button1;
            label1.Text = giris_hakkı.ToString();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void bunifuMaterialTextbox1_OnValueChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
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
            if(giris_hakkı != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgusu = new OleDbCommand("select *from kullanici", baglantim);
                OleDbDataReader kayitoku = selectsorgusu.ExecuteReader();

                while(kayitoku.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitoku["kullaniciadi"].ToString() == bunifuMaterialTextbox1.Text && kayitoku["parola"].ToString() == bunifuMaterialTextbox2.Text && kayitoku["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            ad = kayitoku.GetValue(0).ToString();
                            soyad = kayitoku.GetValue(1).ToString();
                            yetki = kayitoku.GetValue(4).ToString();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            this.Hide();
                            break;

                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (kayitoku["kullaniciadi"].ToString() == bunifuMaterialTextbox1.Text && kayitoku["parola"].ToString() == bunifuMaterialTextbox2.Text && kayitoku["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            ad = kayitoku.GetValue(0).ToString();
                            soyad = kayitoku.GetValue(1).ToString();
                            yetki = kayitoku.GetValue(4).ToString();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            this.Hide();
                            break;

                        }
                    }
                }

                if(durum == false)
                {
                    giris_hakkı--;
                    baglantim.Close();
                }
                label1.Text = giris_hakkı.ToString();
                if(giris_hakkı == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Hakkınız Kalmadı Lütfen Daha sonra tekrar deneyin!!", "BenimSözlüğümHataMesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                
                   


                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }
    }
}
