namespace WIDM_Executie
{
    public partial class Form1 : Form
    {
        public Form1 f1;
        public Form2 f2;
        public Form3 f3;
        public Form4 f4;
        public Form7 f7;

        public Form1()
        {
            InitializeComponent();
            f1 = this;
            f2 = new Form2(this);
            f3 = new Form3(this);
            f4 = new Form4(this);
            f7 = new Form7(this);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f2.Show(this);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f3.UpdateListOfGames();
            this.f3.Show(this);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f4.UpdateListOfGames();
            this.f4.Show(this);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f7.Show(this);
        }
    }
}