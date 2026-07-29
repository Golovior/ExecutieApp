using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;

namespace WIDM_Executie
{
    public partial class Form6 : Form
    {
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

            ExecutionSettings settings = Form6.SettingsForExecution();

            foreach (string speler in this.f4.spelersInExecutie)
            {
                string[] spelerInfo = speler.Split(';');
                if (spelerInfo[0] != textBox1.Text)
                    continue;

                spelerGevonden = true;

                textBox1.Visible = false;
                button1.Visible = false;

                // post_results() wordt zoveel ms eerder verstuurd dan dat het scherm van kleur verandert:
                int lampsOffset = settings.LampsResultDelay;
                string url = settings.LampResultsUrl;

                _ = this.change_roomlights(url, "blinking");

                if (spelerInfo[2] == "y" && settings.ShowYellowScreens)
                {
                    this.ScheduleAfter(settings.SecondsBeforeYellow * 1000 + lampsOffset, () => _ = this.change_roomlights(url, spelerInfo[1]));
                    this.ScheduleAfter(settings.SecondsBeforeYellow * 1000, this.SetYellow);
                }
                if (spelerInfo[1] == "green")
                {
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000 + lampsOffset, () => _ = this.change_roomlights(url, spelerInfo[1]));
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000, this.SetGreen);
                }
                if (spelerInfo[1] == "red")
                {
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000 + lampsOffset, () => _ = this.change_roomlights(url, spelerInfo[1]));
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000, this.SetRed);
                }
                if (spelerInfo[1] == "yellow")
                {
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000 + lampsOffset, () => _ = this.change_roomlights(url, spelerInfo[1]));
                    this.ScheduleAfter(settings.SecondsBeforeColor * 1000, this.SetYellow);
                }

                this.ScheduleAfter(settings.SecondsBeforeColor * 1000 + settings.SecondsOfColor * 1000, this.ResetView);

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

        // Awaiting Task.Delay (instead of Task.Delay(...).ContinueWith(...)) keeps the
        // continuation on the UI SynchronizationContext, so `action` is safe to touch controls.
        // The try/catch keeps a stray failure (e.g. controls disposed because the app closed
        // while a reveal was still pending) from taking down the whole app mid-show.
        private async void ScheduleAfter(int delayMs, Action action)
        {
            await Task.Delay(Math.Max(delayMs, 0));

            try
            {
                action();
            }
            catch (Exception)
            {
                // A scheduled color-change/lamp call failing shouldn't be able to bring down
                // the whole app mid-show; this matches the old fire-and-forget task's behavior.
            }
        }

        private sealed class ExecutionSettings
        {
            public bool ShowYellowScreens;
            public int SecondsBeforeColor;
            public int SecondsOfColor;
            public int SecondsBeforeYellow;
            public int LampsResultDelay = -2000;
            public string LampResultsUrl = "http://localhost:8000";
        }

        private static ExecutionSettings SettingsForExecution()
        {
            Dictionary<string, string> values = ExecutionSettingsStore.Read(ExecutionSettingsStore.FilePath());
            ExecutionSettings settings = new();

            if (values.TryGetValue(ExecutionSettingsStore.ShowYellowScreensKey, out string? showYellow))
                settings.ShowYellowScreens = showYellow == "1";

            if (values.TryGetValue(ExecutionSettingsStore.SecondsBeforeColorKey, out string? sbc))
                int.TryParse(sbc, out settings.SecondsBeforeColor);

            if (values.TryGetValue(ExecutionSettingsStore.SecondsOfColorKey, out string? soc))
                int.TryParse(soc, out settings.SecondsOfColor);

            if (values.TryGetValue(ExecutionSettingsStore.SecondsBeforeYellowKey, out string? sby))
                int.TryParse(sby, out settings.SecondsBeforeYellow);

            // A blank-but-present value matches this app's legacy "blank means 0" convention.
            // Only a fully absent key (never saved, or an old/incomplete file) keeps the -2000 default.
            if (values.TryGetValue(ExecutionSettingsStore.LampsResultDelayKey, out string? delayStr))
            {
                if (delayStr.Length == 0)
                    settings.LampsResultDelay = 0;
                else if (int.TryParse(delayStr, out int delay))
                    settings.LampsResultDelay = delay;
            }

            if (values.TryGetValue(ExecutionSettingsStore.LampResultsUrlKey, out string? url) && ExecutionSettingsStore.IsValidUrl(url))
                settings.LampResultsUrl = url;

            return settings;
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                this.Button1_Click(sender, e);
            }
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
