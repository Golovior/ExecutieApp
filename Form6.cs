using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WIDM_Executie
{
    public partial class Form6 : Form
    {
        // test commit
        public Form4 f4;

        public Form6(Form4 f4)
        {
            InitializeComponent();
            this.f4 = f4;

            textBox1.Focus();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f4.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Exit")
            {
                Label1_Click(sender, e);
                return;
            }

            bool spelerGevonden = false;

            List<int> settings = Form6.SettingsForExecution();

            foreach (string speler in this.f4.spelersInExecutie)
            {
                string[] spelerInfo = speler.Split(';');
                if (spelerInfo[0] != textBox1.Text)
                    continue;

                spelerGevonden = true;

                textBox1.Visible = false;
                button1.Visible = false;

                if (spelerInfo[2] == "y" && settings[0] == 1)
                    Task.Delay(settings[3] * 1000).ContinueWith(t => this.SetYellow());

                if (spelerInfo[1] == "green")
                    Task.Delay(settings[1] * 1000).ContinueWith(t => this.SetGreen());

                if (spelerInfo[1] == "red")
                    Task.Delay(settings[1] * 1000).ContinueWith(t => this.SetRed());

                if (spelerInfo[1] == "yellow")
                    Task.Delay(settings[1] * 1000).ContinueWith(t => this.SetYellow());

                Task.Delay(settings[1] * 1000 + settings[2] * 1000).ContinueWith(t => this.ResetView());

                break;
            }

            if (!spelerGevonden)
                MessageBox.Show("Speler niet gevonden in de lijst.");
        }

        public void SetGreen()
        {
            this.BackgroundImage = global::WIDM_Executie.Properties.Resources.groen;
        }

        public void SetYellow()
        {
            this.BackgroundImage = global::WIDM_Executie.Properties.Resources.geel;
        }

        public void SetRed()
        {
            this.BackgroundImage = global::WIDM_Executie.Properties.Resources.rood;
        }

        public void ResetView()
        {
            this.BackgroundImage = global::WIDM_Executie.Properties.Resources.wie_is_de_mol_2020_1574836707;
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Visible = true;
            button1.Visible = true;
        }

        private static List<int> SettingsForExecution()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            string settingsFile = path + "/executieSettings.txt";

            StreamReader sr = File.OpenText(settingsFile);
            List<int> results = new();

            while (true)
            {
                string s = sr.ReadLine() ?? "";
                if (s == "")
                {
                    break;
                }

                string[] parts = s.Split(',');

                if (parts[1] == "")
                {
                    results.Add(0);
                    continue;
                }

                results.Add(Convert.ToInt32(parts[1]));
            }

            sr.Close();

            return results;
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                this.Button1_Click(sender, e);
            }
        }
    }
}
