using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WIDM_Executie
{
    public partial class Form7 : Form
    {
        readonly Form1 f1;
        public string filePath;
        public string settingsFile;
        public List<int> timerSettings = new();

        public Form7(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;

            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            this.settingsFile = filePath + "/executieSettings.txt";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (!File.Exists(settingsFile))
            {
                var createdFile = File.Create(settingsFile);
                createdFile.Close();
            }

        }

        public void UpdateSettings()
        {
            if (File.Exists(settingsFile))
            {
                StreamReader sr = File.OpenText(settingsFile);

                while (true)
                {
                    string s = sr.ReadLine() ?? "";
                    if (s == "")
                    {
                        break;
                    }

                    string[] parts = s.Split(',');
                    timerSettings.Add(Convert.ToInt32(parts[1]));
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string tempFile = this.filePath + "executieSettingsSave.txt";
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
                List<string> strings = new()
                {
                    "showYellowScreens",
                    "secondsBeforeColor",
                    "secondsOfColor",
                    "secondsBeforeYellow",
                    "lamps_result_delay",
                    
                    // todo: opnieuw aanzetten zodra settings ook strings kunnen bevatten
                    // "resultsUrl"
                };

                foreach (string s in strings)
                {
                    string row = "";
                    switch (s)
                    {
                        case "showYellowScreens":
                            row += "showYellowScreens,";
                            if (checkBox1.Checked)
                            {
                                row += "1";
                            }
                            break;
                        case "secondsBeforeColor":
                            row += "secondsBeforeColor,";
                            row += textBox1.Text;
                            break;
                        case "secondsOfColor":
                            row += "secondsOfColor,";
                            row += textBox2.Text;
                            break;
                        case "secondsBeforeYellow":
                            row += "secondsBeforeYellow,";
                            row += textBox3.Text;
                            break;
                        case "lamp_results_url":
                            row += "lamp_results_url,";
                            row += textBox4.Text;
                            break;
                        case "lamps_result_delay":
                            row += "lamps_result_delay,";
                            row += textBox5.Text;
                            break;
                    }

                    fw.WriteLine(row);
                }

                fw.Flush();

                fw.Close();
            }

            fsW.Close();

            File.Delete(settingsFile);

            File.Copy(tempFile, settingsFile);
            File.Delete(tempFile);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f1.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
