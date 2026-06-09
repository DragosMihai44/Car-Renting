namespace RentCar.Forms
{
    partial class FrmLogin
    {
      
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            lblTitlu = new Label();
            lblUtilizator = new Label();
            txtUtilizator = new TextBox();
            lblParola = new Label();
            txtParola = new TextBox();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            btnAutentificare = new Button();
            btnIesire = new Button();
            lblFooter = new Label();
            timer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblTitlu
            // 
            lblTitlu.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitlu.ForeColor = Color.FromArgb(31, 56, 100);
            lblTitlu.Location = new Point(0, 20);
            lblTitlu.Name = "lblTitlu";
            lblTitlu.Size = new Size(400, 50);
            lblTitlu.TabIndex = 0;
            lblTitlu.Text = "🚗 Rent Car";
            lblTitlu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUtilizator
            // 
            lblUtilizator.AutoSize = true;
            lblUtilizator.Font = new Font("Segoe UI", 10F);
            lblUtilizator.Location = new Point(60, 93);
            lblUtilizator.Name = "lblUtilizator";
            lblUtilizator.Size = new Size(73, 19);
            lblUtilizator.TabIndex = 1;
            lblUtilizator.Text = "Utilizator:";
            // 
            // txtUtilizator
            // 
            txtUtilizator.Font = new Font("Segoe UI", 10F);
            txtUtilizator.Location = new Point(160, 88);
            txtUtilizator.Name = "txtUtilizator";
            txtUtilizator.Size = new Size(200, 25);
            txtUtilizator.TabIndex = 2;
            txtUtilizator.Text = "admin";
            // 
            // lblParola
            // 
            lblParola.AutoSize = true;
            lblParola.Font = new Font("Segoe UI", 10F);
            lblParola.Location = new Point(60, 133);
            lblParola.Name = "lblParola";
            lblParola.Size = new Size(52, 19);
            lblParola.TabIndex = 3;
            lblParola.Text = "Parola:";
            // 
            // txtParola
            // 
            txtParola.Font = new Font("Segoe UI", 10F);
            txtParola.Location = new Point(160, 128);
            txtParola.Name = "txtParola";
            txtParola.PasswordChar = '●';
            txtParola.Size = new Size(200, 25);
            txtParola.TabIndex = 4;
            txtParola.Text = "admin123";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(60, 168);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(300, 22);
            lblStatus.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(60, 196);
            progressBar.Maximum = 20;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(300, 20);
            progressBar.TabIndex = 6;
            // 
            // btnAutentificare
            // 
            btnAutentificare.BackColor = Color.FromArgb(46, 117, 182);
            btnAutentificare.FlatAppearance.BorderSize = 0;
            btnAutentificare.FlatStyle = FlatStyle.Flat;
            btnAutentificare.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAutentificare.ForeColor = Color.White;
            btnAutentificare.Location = new Point(60, 240);
            btnAutentificare.Name = "btnAutentificare";
            btnAutentificare.Size = new Size(140, 38);
            btnAutentificare.TabIndex = 7;
            btnAutentificare.Text = "Autentificare";
            btnAutentificare.UseVisualStyleBackColor = false;
            btnAutentificare.Click += BtnAutentificare_Click;
            // 
            // btnIesire
            // 
            btnIesire.BackColor = Color.FromArgb(200, 60, 60);
            btnIesire.FlatAppearance.BorderSize = 0;
            btnIesire.FlatStyle = FlatStyle.Flat;
            btnIesire.Font = new Font("Segoe UI", 10F);
            btnIesire.ForeColor = Color.White;
            btnIesire.Location = new Point(220, 240);
            btnIesire.Name = "btnIesire";
            btnIesire.Size = new Size(140, 38);
            btnIesire.TabIndex = 8;
            btnIesire.Text = "Ieșire";
            btnIesire.UseVisualStyleBackColor = false;
            btnIesire.Click += BtnIesire_Click;
            // 
            // lblFooter
            // 
            lblFooter.Font = new Font("Segoe UI", 7F);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.Location = new Point(0, 300);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(400, 20);
            lblFooter.TabIndex = 9;
            lblFooter.Text = "© 2025 Rent Car MSOA – Universitatea Politehnică Timișoara";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            timer.Interval = 80;
            timer.Tick += Timer_Tick;
            // 
            // FrmLogin
            // 
            AcceptButton = btnAutentificare;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 252);
            ClientSize = new Size(420, 340);
            Controls.Add(lblFooter);
            Controls.Add(btnIesire);
            Controls.Add(btnAutentificare);
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(txtParola);
            Controls.Add(lblParola);
            Controls.Add(txtUtilizator);
            Controls.Add(lblUtilizator);
            Controls.Add(lblTitlu);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rent Car – Autentificare";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitlu;
        private Label lblUtilizator;
        private TextBox txtUtilizator;
        private Label lblParola;
        private TextBox txtParola;
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnAutentificare;
        private Button btnIesire;
        private Label lblFooter;
        private System.Windows.Forms.Timer timer;
    }
}
