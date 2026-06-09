using RentCar.Data;
using RentCar.Models;
using RentCar.Reports;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Forms;


public class FrmRapoarte : Form
{
    private DateTimePicker dtpStart  = new();
    private DateTimePicker dtpEnd    = new();
    private ComboBox cmbStare        = new();
    private Button btnGenereaza      = new();
    private Button btnExport         = new();
    private ListBox lstRezervari     = new();
    private Label lblTotal           = new();
    private Label lblNr              = new();
    private Label lblMedie           = new();
    private Panel panelStats         = new();

    private readonly RentCarDbContext db;
    private readonly Utilizator utilizator;
    private List<Rezervare> rezervariCurente = new();

    public FrmRapoarte(RentCarDbContext db, Utilizator utilizator)
    {
        this.db         = db;
        this.utilizator = utilizator;
        InitializeComponents();
        GenereazaRaport();
    }

    private void InitializeComponents()
    {
        Text        = "📊 Rapoarte Financiare";
        Size        = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor   = Color.FromArgb(245, 248, 252);

        var filterPanel = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = Color.FromArgb(31,56,100), Padding = new Padding(10, 9, 10, 9) };

        dtpStart.Format = DateTimePickerFormat.Custom;
        dtpStart.CustomFormat = "dd.MM.yyyy";
        dtpStart.Value  = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        dtpEnd.Format   = DateTimePickerFormat.Custom;
        dtpEnd.CustomFormat = "dd.MM.yyyy";
        dtpEnd.Value    = DateTime.Today;

        cmbStare.Items.AddRange(new[] { "(Toate)", "Activa", "Finalizata", "Anulata" });
        cmbStare.SelectedIndex = 0;
        cmbStare.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStare.Size = new Size(120, 26);
        cmbStare.Font = new Font("Segoe UI", 9);

        void AddFlt(string lbl, Control ctrl, ref int x)
        {
            var l = new Label { Text = lbl, Location = new Point(x, 14), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.White };
            ctrl.Location = new Point(x + l.PreferredWidth + 4, 10);
            ctrl.Font = new Font("Segoe UI", 9);
            if (ctrl is DateTimePicker) ctrl.Size = new Size(110, 26);
            filterPanel.Controls.Add(l);
            filterPanel.Controls.Add(ctrl);
            x = ctrl.Right + 14;
        }

        int xPos = 0;
        AddFlt("De la:", dtpStart, ref xPos);
        AddFlt("Până la:", dtpEnd, ref xPos);
        AddFlt("Stare:", cmbStare, ref xPos);

        btnGenereaza = new Button { Text = "🔍 Generează", Location = new Point(xPos, 8), Size = new Size(120, 34),
            Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnGenereaza.FlatAppearance.BorderSize = 0;
        btnGenereaza.Click += (s, e) => GenereazaRaport();

        btnExport = new Button { Text = "💾 Export", Location = new Point(xPos + 130, 8), Size = new Size(110, 34),
            Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(39, 174, 96), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnExport.FlatAppearance.BorderSize = 0;
        btnExport.Click += BtnExport_Click;

        filterPanel.Controls.AddRange(new Control[] { btnGenereaza, btnExport });

       
        panelStats = new Panel { Dock = DockStyle.Bottom, Height = 64, BackColor = Color.FromArgb(230, 240, 255), Padding = new Padding(12, 8, 12, 8) };

        lblTotal = new Label { Location = new Point(12, 8),  AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(31, 56, 100) };
        lblNr    = new Label { Location = new Point(12, 34), AutoSize = true, Font = new Font("Segoe UI", 9),  ForeColor = Color.FromArgb(50, 80, 120) };
        lblMedie = new Label { Location = new Point(300, 34), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(50, 80, 120) };

        panelStats.Controls.AddRange(new Control[] { lblTotal, lblNr, lblMedie });

      
        lstRezervari.Dock        = DockStyle.Fill;
        lstRezervari.Font        = new Font("Courier New", 9);
        lstRezervari.BorderStyle = BorderStyle.None;

       
        var lblHeader = new Label
        {
            Text      = $"{"Nr.",-5} {"Client",-22} {"Vehicul",-22} {"Zile",-5} {"Cost",-12} {"Stare",-12} {"Data Start",-12}",
            Dock      = DockStyle.Top,
            Height    = 26,
            Font      = new Font("Courier New", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(31, 56, 100),
            ForeColor = Color.White,
            Padding   = new Padding(4, 4, 0, 0)
        };

        Controls.Add(lstRezervari);
        Controls.Add(lblHeader);
        Controls.Add(panelStats);
        Controls.Add(filterPanel);
    }

    private void GenereazaRaport()
    {
        var query = db.Rezervari
            .Include(r => r.Client)
            .Include(r => r.Vehicul)
            .Where(r => r.DataStart >= dtpStart.Value.Date && r.DataStart <= dtpEnd.Value.Date);

        if (cmbStare.SelectedIndex > 0 && Enum.TryParse<StareRezervare>(cmbStare.SelectedItem!.ToString(), out var stare))
            query = query.Where(r => r.Stare == stare);

        rezervariCurente = query.OrderBy(r => r.DataStart).ToList();

        lstRezervari.Items.Clear();
        int nr = 1;
        foreach (var r in rezervariCurente)
        {
            string linie = $"{nr,-5} {r.Client?.NumeComplet,-22} " +
                           $"{r.Vehicul?.Marca + " " + r.Vehicul?.Model,-22} " +
                           $"{r.NrZile,-5} {r.CostTotal,-12:C2} {r.Stare,-12} {r.DataStart:dd.MM.yyyy}";
            lstRezervari.Items.Add(linie);
            nr++;
        }

        decimal total = rezervariCurente.Sum(r => r.CostTotal);
        lblTotal.Text = $"Total încasări: {total:C2}";
        lblNr.Text    = $"Număr rezervări: {rezervariCurente.Count}";
        lblMedie.Text = rezervariCurente.Any()
            ? $"Cost mediu: {rezervariCurente.Average(r => r.CostTotal):C2}"
            : "";
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        if (!rezervariCurente.Any())
        { MessageBox.Show("Generați mai întâi un raport!", "Atenție"); return; }

        try
        {
            string cale = ReportHelper.SalveazaRaportFinanciar(
                rezervariCurente, dtpStart.Value, dtpEnd.Value, utilizator.NumeUtilizator);
            var r = MessageBox.Show($"Raport exportat!\n{cale}\n\nDeschideți cu Notepad?",
                    "Export reușit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
                ReportHelper.DeschideInNotepad(cale);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare export: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
