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

        private const int MaxSeconds = 3600;
        private const int MaxLampOffsetMs = 60000;

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

            this.UpdateSettings();
        }

        public void UpdateSettings()
        {
            Dictionary<string, string> values = ExecutionSettingsStore.Read(settingsFile);

            if (values.TryGetValue(ExecutionSettingsStore.ShowYellowScreensKey, out string? showYellow))
                checkBox1.Checked = showYellow == "1";
            if (values.TryGetValue(ExecutionSettingsStore.SecondsBeforeColorKey, out string? secondsBeforeColor))
                textBox1.Text = secondsBeforeColor;
            if (values.TryGetValue(ExecutionSettingsStore.SecondsOfColorKey, out string? secondsOfColor))
                textBox2.Text = secondsOfColor;
            if (values.TryGetValue(ExecutionSettingsStore.SecondsBeforeYellowKey, out string? secondsBeforeYellow))
                textBox3.Text = secondsBeforeYellow;
            if (values.TryGetValue(ExecutionSettingsStore.LampResultsUrlKey, out string? lampResultsUrl))
                textBox4.Text = lampResultsUrl;
            if (values.TryGetValue(ExecutionSettingsStore.LampsResultDelayKey, out string? lampsResultDelay))
                textBox5.Text = lampsResultDelay;
        }

        private static bool TryParseInRange(string text, int min, int max, out int value)
        {
            return int.TryParse(text, out value) && value >= min && value <= max;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!TryParseInRange(textBox1.Text, 0, MaxSeconds, out _) ||
                !TryParseInRange(textBox2.Text, 0, MaxSeconds, out _) ||
                !TryParseInRange(textBox3.Text, 0, MaxSeconds, out _) ||
                !TryParseInRange(textBox5.Text, -MaxLampOffsetMs, MaxLampOffsetMs, out _) ||
                !ExecutionSettingsStore.IsValidUrl(textBox4.Text))
            {
                MessageBox.Show($"Vul geldige waarden in: seconden tussen 0 en {MaxSeconds}, lamps offset tussen -{MaxLampOffsetMs} en {MaxLampOffsetMs} ms, en een geldige URL (http(s)://...) bij Lamps results url.");
                return;
            }

            string tempFile = this.filePath + "/executieSettingsSave.txt";

            using (FileStream fsW = File.Create(tempFile))
            {
                using (var fw = new StreamWriter(fsW))
                {
                    foreach (string key in ExecutionSettingsStore.Keys)
                    {
                        string row = key + ",";

                        if (key == ExecutionSettingsStore.ShowYellowScreensKey)
                        {
                            if (checkBox1.Checked)
                                row += "1";
                        }
                        else if (key == ExecutionSettingsStore.SecondsBeforeColorKey)
                            row += textBox1.Text;
                        else if (key == ExecutionSettingsStore.SecondsOfColorKey)
                            row += textBox2.Text;
                        else if (key == ExecutionSettingsStore.SecondsBeforeYellowKey)
                            row += textBox3.Text;
                        else if (key == ExecutionSettingsStore.LampResultsUrlKey)
                            row += textBox4.Text;
                        else if (key == ExecutionSettingsStore.LampsResultDelayKey)
                            row += textBox5.Text;

                        fw.WriteLine(row);
                    }

                    fw.Flush();
                }
            }

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
