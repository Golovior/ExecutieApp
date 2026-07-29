using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIDM_Executie
{
    public partial class Form4 : Form
    {
        public Form1 f1;
        public Form6 f6;
        public string ExecutieFile = "";
        public string filePath;
        public string filename;
        public List<string> spelersInExecutie = new();

        public Form4(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            this.filename = this.filePath + "/executie.txt";
            this.f6 = new Form6(this);
        }

        public void UpdateListOfGames()
        {
            comboBox1.Items.Clear();

            if (File.Exists(filename))
            {
                using StreamReader sr = File.OpenText(filename);

                while(true)
                {
                    string s = sr.ReadLine() ?? "";
                    if (s == "") {
                        break;
                    }

                    string[] parts = s.Split(',');
                    comboBox1.Items.Add(parts[0]);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.spelersInExecutie.Clear();

            if(comboBox1.Text != "")
            {
                if (!File.Exists(filename))
                {
                    MessageBox.Show("Bestand met spellen niet gevonden.");
                    return;
                }

                using StreamReader sr = File.OpenText(filename);

                while (true)
                {
                    string s = sr.ReadLine() ?? "";
                    if (s == "")
                    {
                        break;
                    }

                    string[] parts = s.Split(',');

                    for(var i = 0; i < parts.Length; i++)
                    {
                        if(i == 0)
                        {
                            if (parts[i] != comboBox1.Text)
                                break;
                        }
                        else
                        {
                            this.spelersInExecutie.Add(parts[i]);
                        }
                    }
                }
            }

            this.Hide();
            this.f6.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f1.Show();
        }
    }
}
