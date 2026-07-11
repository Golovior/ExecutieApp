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
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
                this.SaveStatus();

            this.Hide();
            this.f1.Show();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader sr = File.OpenText(fileName);

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
                    for (Int32 i = 1; i < parts.Length; i++)
                    {
                        string[] playerInfo = parts[i].Split(';');

                        switch(i)
                        {
                            case 1:
                                label2.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp1_g.Checked = true;
                                else
                                    rbp1_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp1_y.Checked = true;
                                break;
                            case 2:
                                label3.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp2_g.Checked = true;
                                else
                                    rbp2_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp2_y.Checked = true;
                                break;
                            case 3:
                                label4.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp3_g.Checked = true;
                                else
                                    rbp3_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp3_y.Checked = true;
                                break;
                            case 4:
                                label5.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp4_g.Checked = true;
                                else
                                    rbp4_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp4_y.Checked = true;
                                break;
                            case 5:
                                label6.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp5_g.Checked = true;
                                else
                                    rbp5_r.Checked = true;
                                 if (playerInfo[2] == "y")
                                    cbp5_y.Checked = true;
                                break;
                            case 6:
                                label7.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp6_g.Checked = true;
                                else
                                    rbp6_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp6_y.Checked = true;
                                break;
                            case 7:
                                label8.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp7_g.Checked = true;
                                else
                                    rbp7_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp7_y.Checked = true;
                                break;
                            case 8:
                                label9.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp8_g.Checked = true;
                                else
                                    rbp8_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp8_y.Checked = true;
                                break;
                            case 9:
                                label10.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp9_g.Checked = true;
                                else
                                    rbp9_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp9_y.Checked = true;
                                break;
                            case 10:
                                label11.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp10_g.Checked = true;
                                else
                                    rbp10_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp10_y.Checked = true;
                                break;
                            case 11:
                                label12.Text = playerInfo[0];
                                if (playerInfo[1] == "green" || playerInfo[1] == "yellow")
                                    rbp11_g.Checked = true;
                                else
                                    rbp11_r.Checked = true;
                                if (playerInfo[2] == "y")
                                    cbp11_y.Checked = true;
                                break;
                        }
                    }
                }
            }
            sr.Close();
        }

        public void UpdateListOfGames()
        {
            if (File.Exists(fileName))
            {
                StreamReader sr = File.OpenText(fileName);

                while (true)
                {
                    string s = sr.ReadLine() ?? "";
                    if (s == "")
                    {
                        break;
                    }

                    string[] parts = s.Split(',');
                    comboBox1.Items.Add(parts[0]);
                }

                sr.Close();
            }
        }

        private void SaveStatus()
        {
            string tempFile = this.filePath + "executieSave.txt";
            FileStream fsW;

            if (File.Exists(tempFile))
            {
                fsW = new FileStream(tempFile, FileMode.Append);
            }
            else
            {
                fsW = File.Create(tempFile);
            }

            using (var fw = new StreamWriter(fsW))
            {
                if (File.Exists(fileName))
                {
                    StreamReader sr = File.OpenText(fileName);

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
                                if (playerInfo[0] == label2.Text)
                                {
                                    if (rbp1_g.Checked) status = "green"; 
                                    if (rbp1_r.Checked) status = "red";
                                    if (rbp1_y.Checked) status = "yellow";
                                    if (cbp1_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label3.Text)
                                {
                                    if (rbp2_g.Checked) status = "green";
                                    if (rbp2_r.Checked) status = "red";
                                    if (rbp2_y.Checked) status = "yellow";
                                    if (cbp2_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label4.Text)
                                {
                                    if (rbp3_g.Checked) status = "green"; 
                                    if (rbp3_r.Checked) status = "red";
                                    if (rbp3_y.Checked) status = "yellow";
                                    if (cbp3_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label5.Text)
                                {
                                    if (rbp4_g.Checked) status = "green"; 
                                    if (rbp4_r.Checked) status = "red";
                                    if (rbp4_y.Checked) status = "yellow";
                                    if (cbp4_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label6.Text)
                                {
                                    if (rbp5_g.Checked) status = "green"; 
                                    if (rbp5_r.Checked) status = "red";
                                    if (rbp5_y.Checked) status = "yellow";
                                    if (cbp5_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label7.Text)
                                {
                                    if (rbp6_g.Checked) status = "green"; 
                                    if (rbp6_r.Checked) status = "red";
                                    if (rbp6_y.Checked) status = "yellow";
                                    if (cbp6_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label8.Text)
                                {
                                    if (rbp7_g.Checked) status = "green"; 
                                    if (rbp7_r.Checked) status = "red";
                                    if (rbp7_y.Checked) status = "yellow";
                                    if (cbp7_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label9.Text)
                                {
                                    if (rbp8_g.Checked) status = "green"; 
                                    if (rbp8_r.Checked) status = "red";
                                    if (rbp8_y.Checked) status = "yellow";
                                    if (cbp8_y.Checked) alreadyYellow = "y";
                                }
                                 
                                if (playerInfo[0] == label10.Text)
                                {
                                    if (rbp9_g.Checked) status = "green"; 
                                    if (rbp9_r.Checked) status = "red";
                                    if (rbp9_y.Checked) status = "yellow";
                                    if (cbp9_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label11.Text)
                                { 
                                    if (rbp10_g.Checked) status = "green"; 
                                    if (rbp10_r.Checked) status = "red";
                                    if (rbp10_y.Checked) status = "yellow";
                                    if (cbp10_y.Checked) alreadyYellow = "y";
                                }

                                if (playerInfo[0] == label12.Text)
                                {
                                    if (rbp11_g.Checked) status = "green"; 
                                    if (rbp11_r.Checked) status = "red";
                                    if (rbp11_y.Checked) status = "yellow";
                                    if (cbp11_y.Checked) alreadyYellow = "y";
                                }

                                executieLine += "," + playerInfo[0] + ";" + status + ";" + alreadyYellow;
                            }

                            fw.WriteLine(executieLine);
                        }
                    }

                    sr.Close();
                }

                fw.Flush();

                fw.Close();
            }

            fsW.Close();

            File.Delete(fileName);

            File.Copy(tempFile, fileName);
            File.Delete(tempFile);
        }
    }
}
