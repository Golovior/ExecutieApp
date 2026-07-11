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
    public partial class Form2 : Form
    {
        public Form1 f1;
        public Form5 f5;
        public List<string> players = new();
        public List<Label> labels = new();

        public Form2(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
            this.f5 = new Form5(this);

            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);
            labels.Add(label10);
            labels.Add(label11);
            labels.Add(label12);
            labels.Add(label13);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.f5.ClearBox();
            this.f5.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            string filename = path + "/executie.txt";
            FileStream fs;

            if (File.Exists(filename))
            {
                fs = new FileStream(filename, FileMode.Append);
            }
            else
            {
                Directory.CreateDirectory(path);
                fs = File.Create(filename);
            }

            using (var fw = new StreamWriter(fs))
            {
                string playerGroup = textBox1.Text;

                foreach(string player in this.players)
                {
                    playerGroup += "," + player + ";green;n";
                }

                fw.WriteLine(playerGroup);
                fw.Flush();

                fw.Close();
            }

            fs.Close();

            this.Hide();
            this.f1.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            f1.Show();
        }

        public void CheckPlayers()
        {
            if(textBox2.Text != "" && this.players.Count == Convert.ToInt32(textBox2.Text))
            {
                this.button2.Enabled = true;
            }

            Int32 i = 0;

            foreach(string player in this.players)
            {
                labels[i].Text = player;
                i++;
            }
        }
    }
}
