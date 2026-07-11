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
    public partial class Form5 : Form
    {
        public Form2 f2;

        public Form5(Form2 f2)
        {
            InitializeComponent();
            this.f2 = f2;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string playerGiven = textBox1.Text;
            foreach(string player in this.f2.players)
            {
                if (player.Equals(playerGiven))
                {
                    MessageBox.Show("Speler al ingegeven. Spelers met dezelfde naam is niet mogelijk.");
                    return;
                }
            }

            this.f2.players.Add(playerGiven);
            f2.CheckPlayers();
            this.Hide();
        }

        public void ClearBox()
        {
            this.textBox1.Text = "";
        }
    }
}
