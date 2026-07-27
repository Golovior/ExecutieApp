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
using System.Net.Http;

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

            List<object> settings = Form6.SettingsForExecution();

            foreach (string speler in this.f4.spelersInExecutie)
            {
                string[] spelerInfo = speler.Split(';');
                if (spelerInfo[0] != textBox1.Text)
                    continue;

                spelerGevonden = true;

                textBox1.Visible = false;
                button1.Visible = false;

                // post_results() wordt zoveel ms eerder verstuurd dan dat het scherm van kleur verandert:
                int lamps_offset = -2000;
                if (settings.Count >= 5 && (int.TryParse(settings[4].ToString(), out int parsedOffset) ) ){
                    lamps_offset = parsedOffset ;
                }

                // default url: localhost
                string url = "http://localhost:8000";
                if (settings.Count > 5 && this.IsValidUrl(settings[5]?.ToString()) )
                {
                    url = settings[5].ToString();
                }
                
                this.change_roomlights(url, "blinking");

                if (spelerInfo[2] == "y" && Convert.ToInt32(settings[0]) == 1 ){ 
                    Task.Delay(Convert.ToInt32(settings[3]) * 1000 + lamps_offset).ContinueWith(t => this.change_roomlights(url, spelerInfo[1]));
                    Task.Delay(Convert.ToInt32(settings[3]) * 1000).ContinueWith(t => this.SetYellow());
                }
                if (spelerInfo[1] == "green" ){
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000 + lamps_offset).ContinueWith(t => this.change_roomlights(url, spelerInfo[1]));
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000).ContinueWith(t => this.SetGreen());
                }
                if (spelerInfo[1] == "red" ){
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000 + lamps_offset).ContinueWith(t => this.change_roomlights(url, spelerInfo[1]));
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000).ContinueWith(t => this.SetRed());
                }
                if (spelerInfo[1] == "yellow" ){
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000 + lamps_offset).ContinueWith(t => this.change_roomlights(url, spelerInfo[1]));
                    Task.Delay(Convert.ToInt32(settings[1]) * 1000).ContinueWith(t => this.SetYellow());
                }
                
                
                Task.Delay(Convert.ToInt32(settings[1]) * 1000 + Convert.ToInt32(settings[2]) * 1000).ContinueWith(t => this.ResetView());

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
        
        
        private static List<object> SettingsForExecution()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            string settingsFile = path + "/executieSettings.txt";

            StreamReader sr = File.OpenText(settingsFile);
            List<object> results = new();

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

                if (int.TryParse(parts[1], out int intValue))
                {
                    results.Add(intValue);
                } else {
                    results.Add(parts[1]);
                }
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
        
        
        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private async Task change_roomlights(string url, string kleur)
        {
            
            using HttpClient client = new HttpClient();

            using FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "kleur", kleur }
                }
            );

            HttpResponseMessage response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
        }
        
        
    }
}
