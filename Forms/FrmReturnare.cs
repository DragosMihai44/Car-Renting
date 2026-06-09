using RentCar.Models;

namespace RentCar.Forms;

public class FrmReturnare : Form
{
    private DateTimePicker dtpDataEfectiva = new();
    private NumericUpDown  nudKmFinal      = new();
    private TextBox        txtObservatii   = new();
    private Label          lblPenalizare   = new();
    private Label          lblInfo         = new();
    private Button         btnOk           = new();
    private Button         btnCancel       = new();

    public DateTime DataEfectiva  => dtpDataEfectiva.Value.Date;
    public int      KmFinal       => (int)nudKmFinal.Value;
    public string   Observatii    => txtObservatii.Text.Trim();
    public decimal  Penalizare    { get; private set; }

    private readonly Rezervare rezervare;

    public FrmReturnare(Rezervare rezervare)
    {
        this.rezervare = rezervare;
        InitializeComponents();
        CalculeazaPenalizare();
    }

    private void InitializeComponents()
    {
        Text            = $"Returnare vehicul – {rezervare.Vehicul?.Marca} {rezervare.Vehicul?.Model}";
        Size            = new Size(420, 340);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = Color.FromArgb(245, 248, 252);

        int lX = 20, cX = 160, cW = 220, rH = 38, sY = 15;

        void AddRow(string lbl, Control ctrl, int row)
        {
            var l = new Label { Text = lbl, Location = new Point(lX, sY + row * rH + 6),
                                Size = new Size(135, 22), Font = new Font("Segoe UI", 9),
                                TextAlign = ContentAlignment.MiddleRight };
            ctrl.Location = new Point(cX, sY + row * rH);
            ctrl.Size     = new Size(cW, 28);
            ctrl.Font     = new Font("Segoe UI", 9);
            Controls.Add(l);
            Controls.Add(ctrl);
        }

       
        lblInfo.Location  = new Point(20, sY);
        lblInfo.Size      = new Size(380, 40);
        lblInfo.Font      = new Font("Segoe UI", 9, FontStyle.Italic);
        lblInfo.ForeColor = Color.FromArgb(46, 117, 182);
        lblInfo.Text      = $"Rezervare #{rezervare.Id} | Retur planificat: {rezervare.DataRetur:dd.MM.yyyy} | Cost: {rezervare.CostTotal:C2}";
        Controls.Add(lblInfo);

        dtpDataEfectiva.Format        = DateTimePickerFormat.Custom;
        dtpDataEfectiva.CustomFormat  = "dd.MM.yyyy";
        dtpDataEfectiva.Value         = DateTime.Today;
        dtpDataEfectiva.ValueChanged += (s, e) => CalculeazaPenalizare();

        nudKmFinal.Minimum = rezervare.Vehicul?.Kilometraj ?? 0;
        nudKmFinal.Maximum = 999999;
        nudKmFinal.Value   = nudKmFinal.Minimum;
        nudKmFinal.ThousandsSeparator = true;

        txtObservatii.Multiline  = true;
        txtObservatii.ScrollBars = ScrollBars.Vertical;
        txtObservatii.Size       = new Size(cW, 52);

        AddRow("Data efectivă:", dtpDataEfectiva, 1);
        AddRow("Km la returnare:", nudKmFinal, 2);

        var lblObs = new Label { Text = "Observații:", Location = new Point(lX, sY + 3 * rH + 4),
                                 Size = new Size(135, 22), Font = new Font("Segoe UI", 9),
                                 TextAlign = ContentAlignment.MiddleRight };
        txtObservatii.Location = new Point(cX, sY + 3 * rH);
        Controls.AddRange(new Control[] { lblObs, txtObservatii });

        lblPenalizare.Location  = new Point(20, sY + 5 * rH - 8);
        lblPenalizare.Size      = new Size(380, 28);
        lblPenalizare.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        lblPenalizare.ForeColor = Color.DarkRed;
        Controls.Add(lblPenalizare);

        btnOk.Text      = "Confirmă returnarea";
        btnOk.Location  = new Point(20, sY + 6 * rH - 8);
        btnOk.Size      = new Size(185, 34);
        btnOk.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        btnOk.BackColor = Color.FromArgb(46, 117, 182);
        btnOk.ForeColor = Color.White;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.Click    += (s, e) => { DialogResult = DialogResult.OK; Close(); };

        btnCancel.Text         = "Anulează";
        btnCancel.Location     = new Point(215, sY + 6 * rH - 8);
        btnCancel.Size         = new Size(165, 34);
        btnCancel.Font         = new Font("Segoe UI", 10);
        btnCancel.FlatStyle    = FlatStyle.Flat;
        btnCancel.DialogResult = DialogResult.Cancel;

        AcceptButton = btnOk;
        CancelButton = btnCancel;
        Controls.AddRange(new Control[] { btnOk, btnCancel });
    }

    private void CalculeazaPenalizare()
    {
        DateTime dataEfectiva = dtpDataEfectiva.Value.Date;
        if (dataEfectiva <= rezervare.DataRetur.Date)
        {
            Penalizare            = 0;
            lblPenalizare.Text    = "✓ Fără penalizare (returnare la timp)";
            lblPenalizare.ForeColor = Color.DarkGreen;
        }
        else
        {
            int zileIntarziere    = (int)(dataEfectiva - rezervare.DataRetur.Date).TotalDays;
            decimal tarifPenaliz  = (rezervare.Vehicul?.TarifZiLei ?? 0) * 1.5m;
            Penalizare            = Math.Round(tarifPenaliz * zileIntarziere, 2);
            lblPenalizare.Text    = $"⚠ Penalizare întârziere ({zileIntarziere} zile): {Penalizare:C2}";
            lblPenalizare.ForeColor = Color.DarkRed;
        }
    }
}
