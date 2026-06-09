using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Forms;


public class FrmService : Form
{
    
    private ListBox  lstToateVehiculele  = new();
    private Button   btnTrimiteInService = new();
    private Label    lblToate            = new();

    
    private FlowLayoutPanel flpAlerte   = new();
    private Label   lblNrAlerte         = new();

    
    private Label   lblDetaliiVehicul   = new();
    private Label   lblAlertaInfo       = new();
    private Button  btnMarcheazaDisponibil = new();
    private Button  btnReincarca        = new();

    private readonly RentCarDbContext db;
    private Vehicul? vehiculSelectat;

    public FrmService(RentCarDbContext db)
    {
        this.db = db;
        InitializeComponents();
        Reincarca();
    }

    private void InitializeComponents()
    {
        Text          = "🔧 Service Vehicule";
        Size          = new Size(1100, 660);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(245, 248, 252);

        
        var header = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 50,
            BackColor = Color.FromArgb(31, 56, 100),
            Padding   = new Padding(12, 8, 12, 8)
        };
        var lblTitle = new Label
        {
            Text      = "🔧 Gestiune Service Vehicule",
            Dock      = DockStyle.Left,
            Width     = 400,
            Font      = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft
        };
        lblNrAlerte = new Label
        {
            Dock      = DockStyle.Right,
            Width     = 220,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(255, 200, 100),
            TextAlign = ContentAlignment.MiddleRight
        };
        btnReincarca = new Button
        {
            Text      = "🔄 Reîncarcă",
            Dock      = DockStyle.Right,
            Width     = 110,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(52, 152, 219),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnReincarca.FlatAppearance.BorderSize = 0;
        btnReincarca.Click += (s, e) => Reincarca();
        header.Controls.AddRange(new Control[] { lblTitle, lblNrAlerte, btnReincarca });

        
        var leftPanel = new Panel
        {
            Dock    = DockStyle.Left,
            Width   = 340,
            Padding = new Padding(6)
        };

        lblToate = new Label
        {
            Text      = "Vehicule disponibile – trimite în service:",
            Dock      = DockStyle.Top,
            Height    = 24,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.FromArgb(31, 56, 100)
        };

        btnTrimiteInService = new Button
        {
            Text      = "🔧 Trimite în service",
            Dock      = DockStyle.Top,
            Height    = 34,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(142, 68, 173),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Enabled   = false
        };
        btnTrimiteInService.FlatAppearance.BorderSize = 0;
        btnTrimiteInService.Click += BtnTrimiteInService_Click;

        lstToateVehiculele = new ListBox
        {
            Dock        = DockStyle.Top,
            Height      = 200,
            Font        = new Font("Segoe UI", 9),
            BorderStyle = BorderStyle.FixedSingle
        };
        lstToateVehiculele.SelectedIndexChanged += (s, e) =>
        {
            btnTrimiteInService.Enabled = lstToateVehiculele.SelectedItem != null;
        };

        
        var lblAlerte = new Label
        {
            Text      = "Vehicule în service și cu alerte:",
            Dock      = DockStyle.Top,
            Height    = 24,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.FromArgb(31, 56, 100)
        };

        var legendPanel = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 22,
            BackColor = Color.FromArgb(240, 245, 255)
        };
        legendPanel.Controls.Add(new Label
        {
            Text     = "🔧 In service   🔴 ITP expirat   🟠 Revizie depășită   🟡 Revizie curând",
            AutoSize = true,
            Location = new Point(4, 4),
            Font     = new Font("Segoe UI", 7),
            ForeColor= Color.FromArgb(60, 60, 80)
        });

        flpAlerte = new FlowLayoutPanel
        {
            Dock          = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            AutoScroll    = true,
            WrapContents  = false,
            BackColor     = Color.White,
            Padding       = new Padding(2)
        };

        leftPanel.Controls.Add(flpAlerte);
        leftPanel.Controls.Add(legendPanel);
        leftPanel.Controls.Add(lblAlerte);
        leftPanel.Controls.Add(btnTrimiteInService);
        leftPanel.Controls.Add(lstToateVehiculele);
        leftPanel.Controls.Add(lblToate);

   
        var rightPanel = new Panel
        {
            Dock       = DockStyle.Fill,
            Padding    = new Padding(16),
            BackColor  = Color.White
        };

        var lblTitluDetalii = new Label
        {
            Text      = "Detalii vehicul selectat",
            Dock      = DockStyle.Top,
            Height    = 28,
            Font      = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(31, 56, 100)
        };

        lblAlertaInfo = new Label
        {
            Dock      = DockStyle.Top,
            Height    = 50,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.DarkRed,
            Padding   = new Padding(0, 4, 0, 4)
        };

        lblDetaliiVehicul = new Label
        {
            Dock      = DockStyle.Top,
            Height    = 170,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(50, 50, 80),
            Text      = "Selectați un vehicul din lista din stânga.",
            Padding   = new Padding(0, 8, 0, 0)
        };

        btnMarcheazaDisponibil = new Button
        {
            Text      = "✅ Marchează ca Disponibil (service finalizat)",
            Dock      = DockStyle.Bottom,
            Height    = 44,
            Font      = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(39, 174, 96),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Enabled   = false
        };
        btnMarcheazaDisponibil.FlatAppearance.BorderSize = 0;
        btnMarcheazaDisponibil.Click += BtnMarcheazaDisponibil_Click;

        rightPanel.Controls.Add(lblDetaliiVehicul);
        rightPanel.Controls.Add(lblAlertaInfo);
        rightPanel.Controls.Add(lblTitluDetalii);
        rightPanel.Controls.Add(btnMarcheazaDisponibil);

        
        var splitter = new Splitter { Dock = DockStyle.Left, Width = 4 };

        Controls.Add(rightPanel);
        Controls.Add(splitter);
        Controls.Add(leftPanel);
        Controls.Add(header);
    }

    

    private void Reincarca()
    {
        vehiculSelectat = null;
        lblDetaliiVehicul.Text      = "Selectați un vehicul din lista din stânga.";
        lblAlertaInfo.Text          = "";
        btnMarcheazaDisponibil.Enabled = false;

        IncarcaListaDisponibile();
        IncarcaAlerteService();
    }

    private void IncarcaListaDisponibile()
    {
        var vehicule = db.Vehicule
            .Include(v => v.Categorie)
            .Where(v => v.Stare == StareVehicul.Disponibil || v.Stare == StareVehicul.Inchiriat)
            .OrderBy(v => v.Marca)
            .ThenBy(v => v.Model)
            .ToList();

        lstToateVehiculele.DataSource    = vehicule;
        lstToateVehiculele.DisplayMember = "ToString";
        btnTrimiteInService.Enabled      = false;
    }


    private void IncarcaAlerteService()
    {
        flpAlerte.Controls.Clear();

        var vehicule = db.Vehicule
            .Include(v => v.Categorie)
            .ToList()
            .Where(v => v.Stare == StareVehicul.InService ||
                        v.ITPExpirat || v.RevizieDepasita ||
                        v.DataReviziei < DateTime.Today.AddMonths(1))
            .OrderBy(v => v.Stare == StareVehicul.InService ? 0 : 1)
            .ThenBy(v => v.DataITP)
            .ToList();

        int inService = vehicule.Count(v => v.Stare == StareVehicul.InService);
        lblNrAlerte.Text = $"{inService} în service  |  {vehicule.Count} total alerte";

        if (!vehicule.Any())
        {
            flpAlerte.Controls.Add(new Label
            {
                Text      = "✅ Nicio alertă activă.\nToate vehiculele sunt în regulă.",
                AutoSize  = false,
                Size      = new Size(310, 50),
                Font      = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                TextAlign = ContentAlignment.MiddleCenter
            });
            return;
        }

        foreach (var v in vehicule)
        {
            Color  bgColor;
            string prefix;

            if (v.Stare == StareVehicul.InService && !v.ITPExpirat && !v.RevizieDepasita)
            { bgColor = Color.FromArgb(52, 152, 219);  prefix = "🔧 IN SERVICE"; }
            else if (v.ITPExpirat)
            { bgColor = Color.FromArgb(192, 57, 43);   prefix = "🔴 ITP EXPIRAT"; }
            else if (v.RevizieDepasita)
            { bgColor = Color.FromArgb(230, 126, 34);  prefix = "🟠 REVIZIE DEPĂȘITĂ"; }
            else
            { bgColor = Color.FromArgb(200, 170, 30);  prefix = "🟡 Revizie curând"; }

            var btn = new Button
            {
                Text      = $"{v.Marca} {v.Model}  –  {v.NrInmatriculare}\n{prefix}",
                Size      = new Size(312, 50),
                Font      = new Font("Segoe UI", 8),
                BackColor = bgColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(6, 0, 0, 0),
                Tag       = v,
                Margin    = new Padding(2)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += BtnVehicul_Click;
            flpAlerte.Controls.Add(btn);
        }
    }

 
    private void BtnTrimiteInService_Click(object? sender, EventArgs e)
    {
        if (lstToateVehiculele.SelectedItem is not Vehicul v) return;

        var result = MessageBox.Show(
            $"Trimiteți vehiculul '{v.Marca} {v.Model} – {v.NrInmatriculare}' în service?",
            "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result != DialogResult.Yes) return;

        var vehiculDb = db.Vehicule.Find(v.Id);
        if (vehiculDb != null)
        {
            vehiculDb.Stare = StareVehicul.InService;
            db.SaveChanges();
            Reincarca();
            MessageBox.Show($"Vehiculul a fost trimis în service.", "Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

   
    private void BtnVehicul_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        vehiculSelectat = (Vehicul)btn.Tag!;

        foreach (Control c in flpAlerte.Controls)
            if (c is Button b) b.FlatAppearance.BorderSize = b == btn ? 2 : 0;

        var alerte = new List<string>();
        if (vehiculSelectat.Stare == StareVehicul.InService)
            alerte.Add("🔧 Vehicul în service");
        if (vehiculSelectat.ITPExpirat)
            alerte.Add($"🔴 ITP expirat din {vehiculSelectat.DataITP:dd.MM.yyyy}");
        if (vehiculSelectat.RevizieDepasita)
            alerte.Add($"🟠 Revizie depășită din {vehiculSelectat.DataReviziei:dd.MM.yyyy}");
        else if (vehiculSelectat.DataReviziei < DateTime.Today.AddMonths(1))
            alerte.Add($"🟡 Revizie programată la {vehiculSelectat.DataReviziei:dd.MM.yyyy}");

        lblAlertaInfo.ForeColor   = vehiculSelectat.ITPExpirat ? Color.DarkRed : Color.FromArgb(31, 56, 100);
        lblAlertaInfo.Text        = string.Join("\n", alerte);
        lblDetaliiVehicul.Text    =
            $"Marcă / Model:   {vehiculSelectat.Marca} {vehiculSelectat.Model} ({vehiculSelectat.AnFabricatie})\n" +
            $"Înmatriculare:   {vehiculSelectat.NrInmatriculare}\n" +
            $"Categorie:       {vehiculSelectat.Categorie?.Denumire}\n" +
            $"Tarif/zi:        {vehiculSelectat.TarifZiLei:C2}\n" +
            $"Kilometraj:      {vehiculSelectat.Kilometraj:N0} km\n" +
            $"Stare curentă:   {vehiculSelectat.Stare.ToDisplay()}\n" +
            $"Data ITP:        {vehiculSelectat.DataITP:dd.MM.yyyy}\n" +
            $"Data reviziei:   {vehiculSelectat.DataReviziei:dd.MM.yyyy}";

        btnMarcheazaDisponibil.Enabled = true;
        btnMarcheazaDisponibil.Text    = vehiculSelectat.Stare == StareVehicul.InService
            ? "✅ Marchează ca Disponibil (service finalizat)"
            : "✅ Actualizează dată ITP și revizie";
    }

 
    private void BtnMarcheazaDisponibil_Click(object? sender, EventArgs e)
    {
        if (vehiculSelectat == null) return;

        var r = MessageBox.Show(
            $"Marcați vehiculul '{vehiculSelectat.Marca} {vehiculSelectat.Model}' ca Disponibil?\n\n" +
            "Se vor actualiza automat data ITP (+1 an) și data reviziei (+6 luni).",
            "Confirmare finalizare service", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (r != DialogResult.Yes) return;

        var v = db.Vehicule.Find(vehiculSelectat.Id);
        if (v != null)
        {
            v.Stare        = StareVehicul.Disponibil;
            v.DataReviziei = DateTime.Today.AddMonths(6);
            v.DataITP      = DateTime.Today.AddYears(1);
            db.SaveChanges();
            MessageBox.Show("Vehiculul a fost marcat ca disponibil!", "Service finalizat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reincarca();
        }
    }
}
