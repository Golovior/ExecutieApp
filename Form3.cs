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
    public partial class Form3 : Form
    {
        public Form1 f1;
        public string ExecutieFile = "";
        public string filePath;
        public string fileName;

        private readonly List<Label> playerLabels;
        private readonly List<RadioButton> greenRadios;
        private readonly List<RadioButton> yellowRadios;
        private readonly List<RadioButton> redRadios;
        private readonly List<CheckBox> alreadyYellowCheckboxes;

        public Form3(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;

            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            this.fileName = filePath + "/executie.txt";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (!File.Exists(fileName))
            {
                var createdFile = File.Create(fileName);
                createdFile.Close();
            }

            playerLabels = new List<Label> { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12 };
            greenRadios = new List<RadioButton> { rbp1_g, rbp2_g, rbp3_g, rbp4_g, rbp5_g, rbp6_g, rbp7_g, rbp8_g, rbp9_g, rbp10_g, rbp11_g };
            yellowRadios = new List<RadioButton> { rbp1_y, rbp2_y, rbp3_y, rbp4_y, rbp5_y, rbp6_y, rbp7_y, rbp8_y, rbp9_y, rbp10_y, rbp11_y };
            redRadios = new List<RadioButton> { rbp1_r, rbp2_r, rbp3_r, rbp4_r, rbp5_r, rbp6_r, rbp7_r, rbp8_r, rbp9_r, rbp10_r, rbp11_r };
            alreadyYellowCheckboxes = new List<CheckBox> { cbp1_y, cbp2_y, cbp3_y, cbp4_y, cbp5_y, cbp6_y, cbp7_y, cbp8_y, cbp9_y, cbp10_y, cbp11_y };
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
                this.SaveStatus();

            this.Hide();
            this.f1.Show();
        }

        private void ResetPlayerControls()
        {
            for (int slot = 0; slot < playerLabels.Count; slot++)
            {
                playerLabels[slot].Text = "";
                greenRadios[slot].Checked = false;
                yellowRadios[slot].Checked = false;
                redRadios[slot].Checked = false;
                alreadyYellowCheckboxes[slot].Checked = false;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPlayerControls();

            using StreamReader sr = File.OpenText(fileName);

            while (true)
            {
                string s = sr.ReadLine() ?? "";
                if (s == "")
                {
                    break;
                }

                string[] parts = s.Split(',');

                if (comboBox1.Text.Equals(parts[0]))
                {
                    for (Int32 i = 1; i < parts.Length && i <= playerLabels.Count; i++)
                    {
                        string[] playerInfo = parts[i].Split(';');
                        int slot = i - 1;

                        playerLabels[slot].Text = playerInfo[0];

                        if (playerInfo[1] == "green")
                            greenRadios[slot].Checked = true;
                        else if (playerInfo[1] == "yellow")
                            yellowRadios[slot].Checked = true;
                        else
                            redRadios[slot].Checked = true;

                        if (playerInfo[2] == "y")
                            alreadyYellowCheckboxes[slot].Checked = true;
                    }
                }
            }
        }

        public void UpdateListOfGames()
        {
            comboBox1.Items.Clear();

            if (File.Exists(fileName))
            {
                using StreamReader sr = File.OpenText(fileName);

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

        private void SaveStatus()
        {
            string tempFile = this.filePath + "/executieSave.txt";

            using FileStream fsW = File.Create(tempFile);

            using (var fw = new StreamWriter(fsW))
            {
                if (File.Exists(fileName))
                {
                    using StreamReader sr = File.OpenText(fileName);

                    while (true)
                    {
                        string s = sr.ReadLine() ?? "";
                        if (s == "")
                        {
                            break;
                        }

                        string[] parts = s.Split(',');

                        if (comboBox1.Text.Equals(parts[0])) {
                            string executieLine = parts[0];

                            for(Int32 i = 1; i < parts.Length; i++)
                            {
                                string[] playerInfo = parts[i].Split(';');
                                string status = "";
                                string alreadyYellow = "n";

                                for (int slot = 0; slot < playerLabels.Count; slot++)
                                {
                                    if (playerInfo[0] != playerLabels[slot].Text)
                                        continue;

                                    if (greenRadios[slot].Checked) status = "green";
                                    if (redRadios[slot].Checked) status = "red";
                                    if (yellowRadios[slot].Checked) status = "yellow";
                                    if (alreadyYellowCheckboxes[slot].Checked) alreadyYellow = "y";
                                    break;
                                }

                                executieLine += "," + playerInfo[0] + ";" + status + ";" + alreadyYellow;
                            }

                            fw.WriteLine(executieLine);
                        }
                    }
                }

                fw.Flush();
            }

            File.Delete(fileName);

            File.Copy(tempFile, fileName);
            File.Delete(tempFile);
        }
    }
}
