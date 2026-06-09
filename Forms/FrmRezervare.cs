using RentCar.Models;
using RentCar.Data;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Forms;


public class FrmRezervare : Form
{
    private ComboBox cmbClient       = new();
    private ComboBox cmbVehicul      = new();
    private DateTimePicker dtpStart  = new();
    private DateTimePicker dtpRetur  = new();
    private TextBox txtLocPreluare   = new();
    private TextBox txtLocReturnare  = new();
    private Label lblCostTotal       = new();
    private Label lblNrZile          = new();
    private Button btnOk             = new();
    private Button btnCancel         = new();
    private ErrorProvider errorProvider = new();

    public int      ClientId        => ((Client)cmbClient.SelectedItem!).Id;
    public int      VehiculId       => ((Vehicul)cmbVehicul.SelectedItem!).Id;
    public DateTime DataStart       => dtpStart.Value.Date;
    public DateTime DataRetur       => dtpRetur.Value.Date;
    public decimal  CostTotal       { get; private set; }
    public string   LocatiePreluare  => txtLocPreluare.Text.Trim();
    public string   LocatieReturnare => txtLocReturnare.Text.Trim();

    private readonly RentCarDbContext db;

    public FrmRezervare(RentCarDbContext db, Rezervare? rezervareExistenta = null)
    {
        this.db = db;
        InitializeComponents();
        PopuleazaDate(rezervareExistenta);
    }

    private void InitializeComponents()
    {
        Text            = "Rezervare nouă";
        Size            = new Size(460, 440);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = Color.FromArgb(245, 248, 252);

        int lX = 20, cX = 145, cW = 270, rH = 38, sY = 15;

        void AddRow(string lbl, Control ctrl, int row)
        {
            var l = new Label { Text = lbl, Location = new Point(lX, sY + row * rH + 6),
                                Size = new Size(120, 22), Font = new Font("Segoe UI", 9),
                                TextAlign = ContentAlignment.MiddleRight };
            ctrl.Location = new Point(cX, sY + row * rH);
            ctrl.Size     = new Size(cW, 28);
            ctrl.Font     = new Font("Segoe UI", 9);
            Controls.Add(l);
            Controls.Add(ctrl);
        }

       
        dtpStart.Format  = DateTimePickerFormat.Custom;
        dtpStart.CustomFormat = "dd.MM.yyyy";
        dtpStart.Value   = DateTime.Today;
        dtpRetur.Format  = DateTimePickerFormat.Custom;
        dtpRetur.CustomFormat = "dd.MM.yyyy";
        dtpRetur.Value   = DateTime.Today.AddDays(3);
        dtpStart.ValueChanged += (s, e) => CalculeazaCost();
        dtpRetur.ValueChanged += (s, e) => CalculeazaCost();

        cmbClient.DropDownStyle  = ComboBoxStyle.DropDownList;
        cmbClient.DisplayMember  = "NumeComplet";
        cmbVehicul.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbVehicul.DisplayMember = "ToString";
        cmbVehicul.SelectedIndexChanged += (s, e) => CalculeazaCost();

        AddRow("Client *:",          cmbClient,       0);
        AddRow("Vehicul *:",         cmbVehicul,      1);
        AddRow("Data start *:",      dtpStart,        2);
        AddRow("Data retur *:",      dtpRetur,        3);
        AddRow("Loc. preluare:",     txtLocPreluare,  4);
        AddRow("Loc. returnare:",    txtLocReturnare, 5);

       
        var panelCost = new Panel
        {
            Location  = new Point(20, sY + 6 * rH + 4),
            Size      = new Size(400, 55),
            BackColor = Color.FromArgb(230, 240, 255),
            BorderStyle = BorderStyle.FixedSingle
        };
        lblNrZile.Location  = new Point(10, 8);
        lblNrZile.Size      = new Size(380, 20);
        lblNrZile.Font      = new Font("Segoe UI", 9);
        lblCostTotal.Location = new Point(10, 28);
        lblCostTotal.Size   = new Size(380, 22);
        lblCostTotal.Font   = new Font("Segoe UI", 11, FontStyle.Bold);
        lblCostTotal.ForeColor = Color.FromArgb(31, 56, 100);
        panelCost.Controls.AddRange(new Control[] { lblNrZile, lblCostTotal });
        Controls.Add(panelCost);

       
        btnOk.Text      = "Confirmă rezervarea";
        btnOk.Location  = new Point(20, sY + 6 * rH + 70);
        btnOk.Size      = new Size(200, 36);
        btnOk.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        btnOk.BackColor = Color.FromArgb(46, 117, 182);
        btnOk.ForeColor = Color.White;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.Click    += BtnOk_Click;

        btnCancel.Text         = "Anulează";
        btnCancel.Location     = new Point(230, sY + 6 * rH + 70);
        btnCancel.Size         = new Size(190, 36);
        btnCancel.Font         = new Font("Segoe UI", 10);
        btnCancel.FlatStyle    = FlatStyle.Flat;
        btnCancel.DialogResult = DialogResult.Cancel;

        AcceptButton = btnOk;
        CancelButton = btnCancel;
        Controls.AddRange(new Control[] { btnOk, btnCancel });
        errorProvider.ContainerControl = this;
    }

    private void PopuleazaDate(Rezervare? rez)
    {
        var clienti  = db.Clienti.ToList();
        var vehicule = db.Vehicule
                         .Include(v => v.Categorie)
                         .Where(v => v.Stare == StareVehicul.Disponibil)
                         .ToList();

        cmbClient.DataSource  = clienti;
        cmbVehicul.DataSource = vehicule;

        if (rez != null)
        {
            Text = "Modifică rezervare";
            cmbClient.SelectedItem  = clienti.FirstOrDefault(c => c.Id == rez.ClientId);
            cmbVehicul.SelectedItem = vehicule.FirstOrDefault(v => v.Id == rez.VehiculId);
            dtpStart.Value          = rez.DataStart;
            dtpRetur.Value          = rez.DataRetur;
            txtLocPreluare.Text     = rez.LocatiePreluare;
            txtLocReturnare.Text    = rez.LocatieReturnare;
        }
        CalculeazaCost();
    }

    private void CalculeazaCost()
    {
        if (cmbVehicul.SelectedItem is not Vehicul v) return;
        if (dtpRetur.Value.Date <= dtpStart.Value.Date)
        {
            lblNrZile.Text   = "⚠ Data de retur trebuie să fie după data de start!";
            lblCostTotal.Text = "Cost: —";
            return;
        }
        int zile    = (int)(dtpRetur.Value.Date - dtpStart.Value.Date).TotalDays;
        CostTotal   = Math.Round(v.TarifZiLei * zile, 2);
        lblNrZile.Text    = $"Număr zile: {zile}  |  Tarif: {v.TarifZiLei:C2}/zi";
        lblCostTotal.Text = $"Cost total: {CostTotal:C2}";
    }

    private void BtnOk_Click(object? sender, EventArgs e)
    {
        errorProvider.Clear();
        bool valid = true;

        if (cmbClient.SelectedItem == null)
        { errorProvider.SetError(cmbClient, "Selectați un client!"); valid = false; }

        if (cmbVehicul.SelectedItem == null)
        { errorProvider.SetError(cmbVehicul, "Selectați un vehicul!"); valid = false; }

        if (dtpRetur.Value.Date <= dtpStart.Value.Date)
        { errorProvider.SetError(dtpRetur, "Data de retur trebuie să fie după data de start!"); valid = false; }

        if (valid)
        {
            CalculeazaCost();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
