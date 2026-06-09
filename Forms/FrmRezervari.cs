using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Forms;


public class FrmRezervari : Form
{
    private ListBox  lstClienti         = new();
    private ListBox  lstRezervari       = new();
    private Button   btnRezervareNoua   = new();
    private Button   btnReturnare       = new();
    private Button   btnAnuleaza        = new();
    private Button   btnStergeRezervare = new();
    private ComboBox cmbFiltruStare     = new();
    private Label    lblInfoRezervare   = new();
    private Label    lblMedia           = new();

    private readonly RentCarDbContext db;
    private readonly Utilizator utilizatorCurent;

    public FrmRezervari(RentCarDbContext db, Utilizator utilizator)
    {
        this.db = db;
        this.utilizatorCurent = utilizator;
        InitializeComponents();
        IncarcaClienti();
    }

    private void InitializeComponents()
    {
        Text        = "📋 Gestiune Rezervări";
        Size        = new Size(1000, 640);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor   = Color.FromArgb(245, 248, 252);

       
        var splitContainer = new SplitContainer
        {
            Dock        = DockStyle.Fill,
            SplitterDistance = 280,
            BorderStyle = BorderStyle.None
        };

        var lblClienti = new Label { Text = "Clienți", Dock = DockStyle.Top, Height = 30,
            Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(31,56,100),
            TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(4,0,0,0) };

        lstClienti.Dock            = DockStyle.Fill;
        lstClienti.Font            = new Font("Segoe UI", 9);
        lstClienti.DisplayMember   = "NumeComplet";
        lstClienti.BorderStyle     = BorderStyle.FixedSingle;
        lstClienti.SelectedIndexChanged += LstClienti_SelectedIndexChanged;

        splitContainer.Panel1.Controls.Add(lstClienti);
        splitContainer.Panel1.Controls.Add(lblClienti);

        var lblRezervari = new Label { Text = "Rezervări client selectat", Dock = DockStyle.Top, Height = 30,
            Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(31,56,100),
            TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(4,0,0,0) };

        var filterPanel = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.FromArgb(220,230,245), Padding = new Padding(4,4,4,4) };
        var lblStare = new Label { Text = "Filtrează:", AutoSize = true, Location = new Point(4,10), Font = new Font("Segoe UI",9) };
        cmbFiltruStare.Location = new Point(70, 6); cmbFiltruStare.Size = new Size(140, 26); cmbFiltruStare.Font = new Font("Segoe UI", 9); cmbFiltruStare.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFiltruStare.Items.AddRange(new[] { "(Toate)", "Activa", "Finalizata", "Anulata" });
        cmbFiltruStare.SelectedIndex = 0;
        cmbFiltruStare.SelectedIndexChanged += (s, e) => IncarcaRezervariClient();
        filterPanel.Controls.AddRange(new Control[] { lblStare, cmbFiltruStare });

        lstRezervari.Dock          = DockStyle.Fill;
        lstRezervari.Font          = new Font("Segoe UI", 9);
        lstRezervari.BorderStyle   = BorderStyle.FixedSingle;
        lstRezervari.SelectedIndexChanged += LstRezervari_SelectedIndexChanged;

        lblInfoRezervare.Dock      = DockStyle.Bottom;
        lblInfoRezervare.Height    = 50;
        lblInfoRezervare.Font      = new Font("Segoe UI", 9, FontStyle.Italic);
        lblInfoRezervare.ForeColor = Color.FromArgb(46, 117, 182);
        lblInfoRezervare.Padding   = new Padding(4);
        lblInfoRezervare.BackColor = Color.FromArgb(230, 240, 255);

        lblMedia.Dock      = DockStyle.Bottom;
        lblMedia.Height    = 24;
        lblMedia.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        lblMedia.ForeColor = Color.DarkGreen;
        lblMedia.Padding   = new Padding(4, 0, 0, 0);

        splitContainer.Panel2.Controls.Add(lstRezervari);
        splitContainer.Panel2.Controls.Add(filterPanel);
        splitContainer.Panel2.Controls.Add(lblRezervari);
        splitContainer.Panel2.Controls.Add(lblMedia);
        splitContainer.Panel2.Controls.Add(lblInfoRezervare);

        var bottomBar = new Panel { Dock = DockStyle.Bottom, Height = 52, BackColor = Color.FromArgb(31,56,100), Padding = new Padding(8,8,8,8) };
        Button MkBtn(string t, Color c) { var b = new Button { Text = t, Height = 34, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(3,0,3,0) }; b.FlatAppearance.BorderSize = 0; return b; }

        btnRezervareNoua   = MkBtn("➕ Rezervare nouă",    Color.FromArgb(39, 174, 96));
        btnReturnare       = MkBtn("🔑 Returnare vehicul", Color.FromArgb(52, 152, 219));
        btnAnuleaza        = MkBtn("✖ Anulează",          Color.FromArgb(230, 126, 34));
        btnStergeRezervare = MkBtn("🗑 Șterge",           Color.FromArgb(192, 57, 43));

        var btnFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        btnFlow.Controls.AddRange(new Control[] { btnRezervareNoua, btnReturnare, btnAnuleaza, btnStergeRezervare });
        bottomBar.Controls.Add(btnFlow);

        Controls.Add(splitContainer);
        Controls.Add(bottomBar);

       
        btnRezervareNoua.Click   += BtnRezervareNoua_Click;
        btnReturnare.Click       += BtnReturnare_Click;
        btnAnuleaza.Click        += BtnAnuleaza_Click;
        btnStergeRezervare.Click += BtnSterge_Click;
    }

   

    private void IncarcaClienti()
    {
        var clienti = db.Clienti.OrderBy(c => c.Nume).ToList();
        lstClienti.DataSource    = clienti;
        lstClienti.DisplayMember = "NumeComplet";
    }

    private void LstClienti_SelectedIndexChanged(object? sender, EventArgs e)
        => IncarcaRezervariClient();

    private void IncarcaRezervariClient()
    {
        if (lstClienti.SelectedItem is not Client client) return;

        var query = db.Rezervari
            .Include(r => r.Vehicul).ThenInclude(v => v!.Categorie)
            .Where(r => r.ClientId == client.Id);

        if (cmbFiltruStare.SelectedIndex > 0)
        {
            string stare = cmbFiltruStare.SelectedItem!.ToString()!;
            if (Enum.TryParse<StareRezervare>(stare, out var stareEnum))
                query = query.Where(r => r.Stare == stareEnum);
        }

        var rezervari = query.OrderByDescending(r => r.DataStart).ToList();
        lstRezervari.DataSource = rezervari;

        
        if (rezervari.Any())
        {
            decimal medie = rezervari.Average(r => r.CostTotal);
            lblMedia.Text = $"Cost mediu rezervări: {medie:C2}  |  Total: {rezervari.Sum(r => r.CostTotal):C2}";
        }
        else
        {
            lblMedia.Text = "";
        }
    }

    private void LstRezervari_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstRezervari.SelectedItem is Rezervare rez)
        {
            lblInfoRezervare.Text =
                $"#{rez.Id} | {rez.Vehicul?.Marca} {rez.Vehicul?.Model} | " +
                $"{rez.DataStart:dd.MM.yyyy} – {rez.DataRetur:dd.MM.yyyy} | " +
                $"{rez.NrZile} zile | Cost: {rez.CostTotal:C2} | {rez.Stare}";
        }
    }

   
    //Crudul mai jos:
    private void BtnRezervareNoua_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmRezervare(db);
        if (frm.ShowDialog() == DialogResult.OK)
        {
            var rez = new Rezervare
            {
                ClientId         = frm.ClientId,
                VehiculId        = frm.VehiculId,
                DataStart        = frm.DataStart,
                DataRetur        = frm.DataRetur,
                CostTotal        = frm.CostTotal,
                LocatiePreluare  = frm.LocatiePreluare,
                LocatieReturnare = frm.LocatieReturnare,
                Stare            = StareRezervare.Activa
            };
            db.Rezervari.Add(rez);

           
            var vehicul = db.Vehicule.Find(frm.VehiculId);
            if (vehicul != null) vehicul.Stare = StareVehicul.Inchiriat;

            db.SaveChanges();
            IncarcaClienti();
            IncarcaRezervariClient();
        }
    }

    private void BtnReturnare_Click(object? sender, EventArgs e)
    {
        if (lstRezervari.SelectedItem is not Rezervare rez)
        { MessageBox.Show("Selectați o rezervare!", "Atenție"); return; }

        if (rez.Stare != StareRezervare.Activa)
        { MessageBox.Show("Doar rezervările active pot fi returnate!", "Atenție"); return; }

        var rezervareFull = db.Rezervari
            .Include(r => r.Vehicul)
            .First(r => r.Id == rez.Id);

        using var frm = new FrmReturnare(rezervareFull);
        if (frm.ShowDialog() == DialogResult.OK)
        {
           
            var returnare = new Returnare
            {
                RezervareId  = rez.Id,
                DataEfectiva = frm.DataEfectiva,
                KmFinal      = frm.KmFinal,
                Observatii   = frm.Observatii,
                Penalizare   = frm.Penalizare
            };
            db.Returnari.Add(returnare);

          
            rezervareFull.Stare = StareRezervare.Finalizata;
            if (rezervareFull.Vehicul != null)
            {
                rezervareFull.Vehicul.Stare      = StareVehicul.Disponibil;
                rezervareFull.Vehicul.Kilometraj = frm.KmFinal;
            }

            db.SaveChanges();
            IncarcaRezervariClient();

            string msg = frm.Penalizare > 0
                ? $"Returnare procesată!\nPenalizare întârziere: {frm.Penalizare:C2}"
                : "Returnare procesată cu succes!";
            MessageBox.Show(msg, "Returnare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnAnuleaza_Click(object? sender, EventArgs e)
    {
        if (lstRezervari.SelectedItem is not Rezervare rez)
        { MessageBox.Show("Selectați o rezervare!", "Atenție"); return; }

        if (rez.Stare != StareRezervare.Activa)
        { MessageBox.Show("Doar rezervările active pot fi anulate!", "Atenție"); return; }

        var r = MessageBox.Show($"Anulați rezervarea #{rez.Id}?", "Confirmare",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (r == DialogResult.Yes)
        {
            var rezervareFull = db.Rezervari.Include(rv => rv.Vehicul).First(rv => rv.Id == rez.Id);
            rezervareFull.Stare = StareRezervare.Anulata;
            if (rezervareFull.Vehicul != null)
                rezervareFull.Vehicul.Stare = StareVehicul.Disponibil;
            db.SaveChanges();
            IncarcaRezervariClient();
        }
    }

    private void BtnSterge_Click(object? sender, EventArgs e)
    {
        if (lstRezervari.SelectedItem is not Rezervare rez)
        { MessageBox.Show("Selectați o rezervare!", "Atenție"); return; }

        var r = MessageBox.Show($"Ștergeți definitiv rezervarea #{rez.Id}?", "Confirmare",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (r == DialogResult.Yes)
        {
            db.Rezervari.Remove(rez);
            db.SaveChanges();
            IncarcaRezervariClient();
        }
    }
}
