namespace RentCar.Forms
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabMain = new TabControl();
            menuStrip = new MenuStrip();
            mnuFisier = new ToolStripMenuItem();
            mnuIesire = new ToolStripMenuItem();
            mnuAjutor = new ToolStripMenuItem();
            mnuDespre = new ToolStripMenuItem();
            statusBar = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblUser   = new ToolStripStatusLabel();
            lblTime   = new ToolStripStatusLabel();
            timerCeas = new System.Windows.Forms.Timer(components);
            menuStrip.SuspendLayout();
            statusBar.SuspendLayout();
            SuspendLayout();
            //
            // tabMain
            //
            tabMain.Dock     = DockStyle.Fill;
            tabMain.Font     = new Font("Segoe UI", 10F);
            tabMain.ItemSize = new Size(130, 34);
            tabMain.Location = new Point(0, 28);
            tabMain.Name     = "tabMain";
            tabMain.SizeMode = TabSizeMode.Fixed;
            tabMain.TabIndex = 0;
            //
            // menuStrip
            //
            menuStrip.BackColor = Color.FromArgb(31, 56, 100);
            menuStrip.ForeColor = Color.White;
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuFisier, mnuAjutor });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name     = "menuStrip";
            menuStrip.Size     = new Size(1280, 28);
            menuStrip.TabIndex = 1;
            //
            // mnuFisier
            //
            mnuFisier.DropDownItems.AddRange(new ToolStripItem[] { mnuIesire });
            mnuFisier.ForeColor = Color.White;
            mnuFisier.Name = "mnuFisier";
            mnuFisier.Text = "&Fișier";
            //
            // mnuIesire
            //
            mnuIesire.Name   = "mnuIesire";
            mnuIesire.Text   = "Ieșire";
            mnuIesire.Click += MnuIesire_Click;
            //
            // mnuAjutor
            //
            mnuAjutor.DropDownItems.AddRange(new ToolStripItem[] { mnuDespre });
            mnuAjutor.ForeColor = Color.White;
            mnuAjutor.Name = "mnuAjutor";
            mnuAjutor.Text = "&Ajutor";
            //
            // mnuDespre
            //
            mnuDespre.Name   = "mnuDespre";
            mnuDespre.Text   = "Despre";
            mnuDespre.Click += MnuDespre_Click;
            //
            // statusBar
            //
            statusBar.BackColor = Color.FromArgb(31, 56, 100);
            statusBar.ForeColor = Color.White;
            statusBar.Items.AddRange(new ToolStripItem[] { lblStatus, lblUser, lblTime });
            statusBar.Location = new Point(0, 710);
            statusBar.Name     = "statusBar";
            statusBar.Size     = new Size(1280, 22);
            statusBar.TabIndex = 2;
            //
            // lblStatus
            //
            lblStatus.Name   = "lblStatus";
            lblStatus.Spring = true;
            lblStatus.Text   = "";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblUser
            //
            lblUser.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblUser.Name = "lblUser";
            lblUser.Text = "";
            //
            // lblTime
            //
            lblTime.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblTime.Name = "lblTime";
            lblTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            //
            // timerCeas
            //
            timerCeas.Interval = 1000;
            timerCeas.Tick    += TimerCeas_Tick;
            //
            // FrmPrincipal
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor     = Color.FromArgb(245, 248, 252);
            ClientSize    = new Size(1280, 732);
            Controls.Add(tabMain);
            Controls.Add(menuStrip);
            Controls.Add(statusBar);
            MainMenuStrip = menuStrip;
            Name          = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text          = "Rent Car";
            WindowState   = FormWindowState.Maximized;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TabControl tabMain;
        private MenuStrip menuStrip;
        private ToolStripMenuItem mnuFisier;
        private ToolStripMenuItem mnuIesire;
        private ToolStripMenuItem mnuAjutor;
        private ToolStripMenuItem mnuDespre;
        private StatusStrip statusBar;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblUser;
        private ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Timer timerCeas;
    }
}
