namespace WIDM_Executie
{
    partial class Form6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(0, 0);
            label1.MinimumSize = new Size(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(80, 80);
            label1.TabIndex = 0;
            label1.Click += Label1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(886, 906);
            button1.Name = "button1";
            button1.Size = new Size(186, 74);
            button1.TabIndex = 1;
            button1.Text = "Invoeren";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(817, 795);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(312, 71);
            textBox1.TabIndex = 2;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.KeyDown += TextBox1_KeyUp;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.BackColor = Color.Transparent;
            label2.CausesValidation = false;
            label2.Location = new Point(1840, 0);
            label2.MinimumSize = new Size(30, 30);
            label2.Name = "label2";
            label2.Size = new Size(80, 80);
            label2.TabIndex = 3;
            label2.Click += Label2_Click;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.wie_is_de_mol_2020_1574836707;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1920, 1080);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form6";
            Text = "Form6";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
    }
}