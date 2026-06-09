namespace RentCar.Forms
{
    partial class FrmVehicul
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
            txtMarca = new TextBox();
            txtModel = new TextBox();
            txtNrInmatriculare = new TextBox();
            nudAn = new NumericUpDown();
            nudTarif = new NumericUpDown();
            nudKm = new NumericUpDown();
            cmbCategorie = new ComboBox();
            cmbStare = new ComboBox();
            dtpReviziei = new DateTimePicker();
            dtpITP = new DateTimePicker();
            btnOk = new Button();
            btnCancel = new Button();
            errorProvider = new ErrorProvider(components);
            lblMarca = new Label();
            lblModel = new Label();
            lblNr = new Label();
            lblAn = new Label();
            lblCategorie = new Label();
            lblTarif = new Label();
            lblKm = new Label();
            lblStare = new Label();
            lblReviziei = new Label();
            lblITP = new Label();
            ((System.ComponentModel.ISupportInitialize)nudAn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTarif).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudKm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // txtMarca
            txtMarca.Font = new Font("Segoe UI", 9F);
            txtMarca.Location = new Point(145, 15);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(250, 23);
            txtMarca.TabIndex = 1;
            // txtModel
            txtModel.Font = new Font("Segoe UI", 9F);
            txtModel.Location = new Point(145, 51);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(250, 23);
            txtModel.TabIndex = 3;
            // txtNrInmatriculare
            txtNrInmatriculare.CharacterCasing = CharacterCasing.Upper;
            txtNrInmatriculare.Font = new Font("Segoe UI", 9F);
            txtNrInmatriculare.Location = new Point(145, 87);
            txtNrInmatriculare.Name = "txtNrInmatriculare";
            txtNrInmatriculare.Size = new Size(250, 23);
            txtNrInmatriculare.TabIndex = 5;
            // nudAn
            nudAn.Font = new Font("Segoe UI", 9F);
            nudAn.Location = new Point(145, 123);
            nudAn.Maximum = new decimal(new int[] { 2030, 0, 0, 0 });
            nudAn.Minimum = new decimal(new int[] { 1990, 0, 0, 0 });
            nudAn.Name = "nudAn";
            nudAn.Size = new Size(250, 23);
            nudAn.TabIndex = 7;
            nudAn.Value = new decimal(new int[] { 2022, 0, 0, 0 });
            // nudTarif
            nudTarif.DecimalPlaces = 2;
            nudTarif.Font = new Font("Segoe UI", 9F);
            nudTarif.Location = new Point(145, 195);
            nudTarif.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudTarif.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            nudTarif.Name = "nudTarif";
            nudTarif.Size = new Size(250, 23);
            nudTarif.TabIndex = 11;
            nudTarif.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // nudKm
            nudKm.Font = new Font("Segoe UI", 9F);
            nudKm.Location = new Point(145, 231);
            nudKm.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudKm.Name = "nudKm";
            nudKm.Size = new Size(250, 23);
            nudKm.TabIndex = 13;
            nudKm.ThousandsSeparator = true;
            // cmbCategorie
            cmbCategorie.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategorie.Font = new Font("Segoe UI", 9F);
            cmbCategorie.Location = new Point(145, 159);
            cmbCategorie.Name = "cmbCategorie";
            cmbCategorie.Size = new Size(250, 23);
            cmbCategorie.TabIndex = 9;
            // cmbStare
            cmbStare.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStare.Font = new Font("Segoe UI", 9F);
            cmbStare.Items.AddRange(new object[] { "Disponibil", "Închiriat", "In service" });
            cmbStare.Location = new Point(145, 267);
            cmbStare.Name = "cmbStare";
            cmbStare.Size = new Size(250, 23);
            cmbStare.TabIndex = 15;
            cmbStare.SelectedIndex = 0;
            // dtpReviziei
            dtpReviziei.Font = new Font("Segoe UI", 9F);
            dtpReviziei.Format = DateTimePickerFormat.Custom;
            dtpReviziei.CustomFormat = "dd.MM.yyyy";
            dtpReviziei.Location = new Point(145, 303);
            dtpReviziei.Name = "dtpReviziei";
            dtpReviziei.Size = new Size(250, 23);
            dtpReviziei.TabIndex = 17;
            dtpReviziei.Value = DateTime.Today.AddMonths(6);
            // dtpITP
            dtpITP.Font = new Font("Segoe UI", 9F);
            dtpITP.Format = DateTimePickerFormat.Custom;
            dtpITP.CustomFormat = "dd.MM.yyyy";
            dtpITP.Location = new Point(145, 339);
            dtpITP.Name = "dtpITP";
            dtpITP.Size = new Size(250, 23);
            dtpITP.TabIndex = 19;
            dtpITP.Value = DateTime.Today.AddYears(1);
            // btnOk
            btnOk.BackColor = Color.FromArgb(46, 117, 182);
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(145, 382);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(120, 34);
            btnOk.TabIndex = 20;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;
            // btnCancel
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(275, 382);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 34);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Anulează";
            btnCancel.Click += BtnCancel_Click;
            // Labels
            lblMarca    = MkLbl("Marcă *:",            15);
            lblModel    = MkLbl("Model *:",             51);
            lblNr       = MkLbl("Nr. Înmatriculare *:", 87);
            lblAn       = MkLbl("An fabricație:",       123);
            lblCategorie= MkLbl("Categorie:",           159);
            lblTarif    = MkLbl("Tarif/zi (lei):",      195);
            lblKm       = MkLbl("Kilometraj:",          231);
            lblStare    = MkLbl("Stare:",               267);
            lblReviziei = MkLbl("Data reviziei:",       303);
            lblITP      = MkLbl("Data ITP:",            339);
            // errorProvider
            errorProvider.ContainerControl = this;
            // FrmVehicul
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 252);
            ClientSize = new Size(430, 435);
            Controls.Add(txtMarca); Controls.Add(txtModel); Controls.Add(txtNrInmatriculare);
            Controls.Add(nudAn); Controls.Add(cmbCategorie); Controls.Add(nudTarif);
            Controls.Add(nudKm); Controls.Add(cmbStare); Controls.Add(dtpReviziei);
            Controls.Add(dtpITP); Controls.Add(btnOk); Controls.Add(btnCancel);
            Controls.Add(lblMarca); Controls.Add(lblModel); Controls.Add(lblNr);
            Controls.Add(lblAn); Controls.Add(lblCategorie); Controls.Add(lblTarif);
            Controls.Add(lblKm); Controls.Add(lblStare); Controls.Add(lblReviziei);
            Controls.Add(lblITP);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            Name = "FrmVehicul";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Vehicul";
            ((System.ComponentModel.ISupportInitialize)nudAn).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTarif).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudKm).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label MkLbl(string text, int y)
        {
            var l = new Label { Text = text, Location = new Point(20, y + 4), Size = new Size(120, 19), Font = new Font("Segoe UI", 9F), TextAlign = ContentAlignment.MiddleRight };
            return l;
        }
        #endregion

        private TextBox txtMarca, txtModel, txtNrInmatriculare;
        private NumericUpDown nudAn, nudTarif, nudKm;
        private ComboBox cmbCategorie, cmbStare;
        private DateTimePicker dtpReviziei, dtpITP;
        private Button btnOk, btnCancel;
        private ErrorProvider errorProvider;
        private Label lblMarca, lblModel, lblNr, lblAn, lblCategorie, lblTarif, lblKm, lblStare, lblReviziei, lblITP;
    }
}
