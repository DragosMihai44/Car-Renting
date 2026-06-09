namespace RentCar.Forms
{
    partial class FrmFlota
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
            dgvVehicule = new DataGridView();
            toolbar = new Panel();
            toolFlow = new FlowLayoutPanel();
            btnAdauga = new Button();
            btnModifica = new Button();
            btnSterge = new Button();
            btnActualizeazaStare = new Button();
            btnSortTarif = new Button();
            btnSortKm = new Button();
            btnReset = new Button();
            filterPanel = new Panel();
            lblCat = new Label();
            cmbFiltruCategorie = new ComboBox();
            lblStare = new Label();
            cmbFiltruStare = new ComboBox();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVehicule).BeginInit();
            toolbar.SuspendLayout();
            toolFlow.SuspendLayout();
            filterPanel.SuspendLayout();
            SuspendLayout();
            // dgvVehicule
            dgvVehicule.AllowUserToAddRows = false;
            dgvVehicule.AllowUserToDeleteRows = false;
            dgvVehicule.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
            dgvVehicule.AutoGenerateColumns = true;
            dgvVehicule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvVehicule.BackgroundColor = Color.White;
            dgvVehicule.BorderStyle = BorderStyle.None;
            dgvVehicule.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 56, 100);
            dgvVehicule.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvVehicule.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVehicule.Dock = DockStyle.Fill;
            dgvVehicule.EnableHeadersVisualStyles = false;
            dgvVehicule.Location = new Point(0, 92);
            dgvVehicule.MultiSelect = false;
            dgvVehicule.Name = "dgvVehicule";
            dgvVehicule.ReadOnly = true;
            dgvVehicule.RowHeadersVisible = false;
            dgvVehicule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVehicule.Size = new Size(960, 528);
            dgvVehicule.TabIndex = 0;
            // toolbar
            toolbar.BackColor = Color.FromArgb(31, 56, 100);
            toolbar.Controls.Add(toolFlow);
            toolbar.Dock = DockStyle.Top;
            toolbar.Location = new Point(0, 0);
            toolbar.Name = "toolbar";
            toolbar.Padding = new Padding(8, 6, 8, 6);
            toolbar.Size = new Size(960, 48);
            toolbar.TabIndex = 1;
            // toolFlow
            toolFlow.Controls.Add(btnAdauga);
            toolFlow.Controls.Add(btnModifica);
            toolFlow.Controls.Add(btnSterge);
            toolFlow.Controls.Add(btnActualizeazaStare);
            toolFlow.Controls.Add(btnSortTarif);
            toolFlow.Controls.Add(btnSortKm);
            toolFlow.Controls.Add(btnReset);
            toolFlow.Dock = DockStyle.Fill;
            toolFlow.Location = new Point(8, 6);
            toolFlow.Name = "toolFlow";
            toolFlow.Size = new Size(944, 36);
            toolFlow.TabIndex = 0;
            // btnAdauga
            btnAdauga.AutoSize = true;
            btnAdauga.BackColor = Color.FromArgb(39, 174, 96);
            btnAdauga.FlatAppearance.BorderSize = 0;
            btnAdauga.FlatStyle = FlatStyle.Flat;
            btnAdauga.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdauga.ForeColor = Color.White;
            btnAdauga.Height = 34;
            btnAdauga.Margin = new Padding(3, 0, 3, 0);
            btnAdauga.Name = "btnAdauga";
            btnAdauga.TabIndex = 0;
            btnAdauga.Text = "➕ Adaugă";
            btnAdauga.UseVisualStyleBackColor = false;
            btnAdauga.Click += BtnAdauga_Click;
            // btnModifica
            btnModifica.AutoSize = true;
            btnModifica.BackColor = Color.FromArgb(52, 152, 219);
            btnModifica.FlatAppearance.BorderSize = 0;
            btnModifica.FlatStyle = FlatStyle.Flat;
            btnModifica.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnModifica.ForeColor = Color.White;
            btnModifica.Height = 34;
            btnModifica.Margin = new Padding(3, 0, 3, 0);
            btnModifica.Name = "btnModifica";
            btnModifica.TabIndex = 1;
            btnModifica.Text = "✏ Modifică";
            btnModifica.UseVisualStyleBackColor = false;
            btnModifica.Click += BtnModifica_Click;
            // btnSterge
            btnSterge.AutoSize = true;
            btnSterge.BackColor = Color.FromArgb(192, 57, 43);
            btnSterge.FlatAppearance.BorderSize = 0;
            btnSterge.FlatStyle = FlatStyle.Flat;
            btnSterge.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSterge.ForeColor = Color.White;
            btnSterge.Height = 34;
            btnSterge.Margin = new Padding(3, 0, 3, 0);
            btnSterge.Name = "btnSterge";
            btnSterge.TabIndex = 2;
            btnSterge.Text = "🗑 Șterge";
            btnSterge.UseVisualStyleBackColor = false;
            btnSterge.Click += BtnSterge_Click;
            // btnActualizeazaStare
            btnActualizeazaStare.AutoSize = true;
            btnActualizeazaStare.BackColor = Color.FromArgb(142, 68, 173);
            btnActualizeazaStare.FlatAppearance.BorderSize = 0;
            btnActualizeazaStare.FlatStyle = FlatStyle.Flat;
            btnActualizeazaStare.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActualizeazaStare.ForeColor = Color.White;
            btnActualizeazaStare.Height = 34;
            btnActualizeazaStare.Margin = new Padding(3, 0, 3, 0);
            btnActualizeazaStare.Name = "btnActualizeazaStare";
            btnActualizeazaStare.TabIndex = 3;
            btnActualizeazaStare.Text = "🔄 Actualizează Stare";
            btnActualizeazaStare.UseVisualStyleBackColor = false;
            btnActualizeazaStare.Click += BtnActualizeazaStare_Click;
            // btnSortTarif
            btnSortTarif.AutoSize = true;
            btnSortTarif.BackColor = Color.FromArgb(41, 128, 185);
            btnSortTarif.FlatAppearance.BorderSize = 0;
            btnSortTarif.FlatStyle = FlatStyle.Flat;
            btnSortTarif.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSortTarif.ForeColor = Color.White;
            btnSortTarif.Height = 34;
            btnSortTarif.Margin = new Padding(3, 0, 3, 0);
            btnSortTarif.Name = "btnSortTarif";
            btnSortTarif.TabIndex = 4;
            btnSortTarif.Text = "💰 Sort Tarif";
            btnSortTarif.UseVisualStyleBackColor = false;
            btnSortTarif.Click += BtnSortTarif_Click;
            // btnSortKm
            btnSortKm.AutoSize = true;
            btnSortKm.BackColor = Color.FromArgb(41, 128, 185);
            btnSortKm.FlatAppearance.BorderSize = 0;
            btnSortKm.FlatStyle = FlatStyle.Flat;
            btnSortKm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSortKm.ForeColor = Color.White;
            btnSortKm.Height = 34;
            btnSortKm.Margin = new Padding(3, 0, 3, 0);
            btnSortKm.Name = "btnSortKm";
            btnSortKm.TabIndex = 5;
            btnSortKm.Text = "📏 Sort Km";
            btnSortKm.UseVisualStyleBackColor = false;
            btnSortKm.Click += BtnSortKm_Click;
            // btnReset
            btnReset.AutoSize = true;
            btnReset.BackColor = Color.FromArgb(127, 140, 141);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Height = 34;
            btnReset.Margin = new Padding(3, 0, 3, 0);
            btnReset.Name = "btnReset";
            btnReset.TabIndex = 6;
            btnReset.Text = "↺ Reset Filtre";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            // filterPanel
            filterPanel.BackColor = Color.FromArgb(220, 230, 245);
            filterPanel.Controls.Add(lblCat);
            filterPanel.Controls.Add(cmbFiltruCategorie);
            filterPanel.Controls.Add(lblStare);
            filterPanel.Controls.Add(cmbFiltruStare);
            filterPanel.Controls.Add(lblTotal);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Location = new Point(0, 48);
            filterPanel.Name = "filterPanel";
            filterPanel.Padding = new Padding(8);
            filterPanel.Size = new Size(960, 44);
            filterPanel.TabIndex = 2;
            // lblCat
            lblCat.AutoSize = true;
            lblCat.Font = new Font("Segoe UI", 9F);
            lblCat.Location = new Point(8, 14);
            lblCat.Name = "lblCat";
            lblCat.Text = "Categorie:";
            // cmbFiltruCategorie
            cmbFiltruCategorie.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltruCategorie.Font = new Font("Segoe UI", 9F);
            cmbFiltruCategorie.Location = new Point(80, 10);
            cmbFiltruCategorie.Name = "cmbFiltruCategorie";
            cmbFiltruCategorie.Size = new Size(160, 23);
            cmbFiltruCategorie.TabIndex = 0;
            cmbFiltruCategorie.SelectedIndexChanged += CmbFiltru_SelectedIndexChanged;
            // lblStare
            lblStare.AutoSize = true;
            lblStare.Font = new Font("Segoe UI", 9F);
            lblStare.Location = new Point(255, 14);
            lblStare.Name = "lblStare";
            lblStare.Text = "Stare:";
            // cmbFiltruStare
            cmbFiltruStare.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltruStare.Font = new Font("Segoe UI", 9F);
            cmbFiltruStare.Items.AddRange(new object[] { "(Toate)", "Disponibil", "Închiriat", "In service" });
            cmbFiltruStare.Location = new Point(300, 10);
            cmbFiltruStare.Name = "cmbFiltruStare";
            cmbFiltruStare.Size = new Size(140, 23);
            cmbFiltruStare.TabIndex = 1;
            cmbFiltruStare.SelectedIndex = 0;
            cmbFiltruStare.SelectedIndexChanged += CmbFiltru_SelectedIndexChanged;
            // lblTotal
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(31, 56, 100);
            lblTotal.Location = new Point(460, 14);
            lblTotal.Name = "lblTotal";
            lblTotal.Text = "Total: 0 vehicule";
            // FrmFlota
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 252);
            ClientSize = new Size(960, 620);
            Controls.Add(dgvVehicule);
            Controls.Add(filterPanel);
            Controls.Add(toolbar);
            MinimumSize = new Size(800, 500);
            Name = "FrmFlota";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🚗 Gestiune Flotă";
            ((System.ComponentModel.ISupportInitialize)dgvVehicule).EndInit();
            toolbar.ResumeLayout(false);
            toolFlow.ResumeLayout(false);
            toolFlow.PerformLayout();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private DataGridView dgvVehicule;
        private Panel toolbar;
        private FlowLayoutPanel toolFlow;
        private Button btnAdauga;
        private Button btnModifica;
        private Button btnSterge;
        private Button btnActualizeazaStare;
        private Button btnSortTarif;
        private Button btnSortKm;
        private Button btnReset;
        private Panel filterPanel;
        private Label lblCat;
        private ComboBox cmbFiltruCategorie;
        private Label lblStare;
        private ComboBox cmbFiltruStare;
        private Label lblTotal;
    }
}
