using RentCar.Comparers;
using RentCar.Data;
using RentCar.Models;
using RentCar.Reports;

namespace RentCar.Forms;


public class FrmClienti : Form
{
    private TreeView     tvwClienti  = new();
    private PropertyGrid pgdClient  = new();
    private TextBox      txtCauta   = new();
    private Button       btnCauta   = new();
    private Button       btnAdauga  = new();
    private Button       btnModifica = new();
    private Button       btnSterge  = new();
    private Button       btnSalveaza = new();
    private Button       btnSortAZ   = new();
    private Panel        panelActiuni = new();

    private readonly RentCarDbContext db;
    private List<Client> clienti = new();

    public FrmClienti(RentCarDbContext db)
    {
        this.db = db;
        InitializeComponents();
        IncarcaClienti();
    }

    private void InitializeComponents()
    {
        Text        = "👥 Gestiune Clienți";
        Size        = new Size(960, 620);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor   = Color.FromArgb(245, 248, 252);

       
        var leftPanel = new Panel { Dock = DockStyle.Left, Width = 320, Padding = new Padding(8) };

    
        var searchPanel = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.FromArgb(220, 230, 245), Padding = new Padding(4) };
        txtCauta.Location = new Point(4, 8); txtCauta.Size = new Size(200, 26); txtCauta.Font = new Font("Segoe UI", 9); txtCauta.PlaceholderText = "Caută client...";
        btnCauta.Text     = "🔍"; btnCauta.Location = new Point(208, 7); btnCauta.Size = new Size(50, 28); btnCauta.FlatStyle = FlatStyle.Flat; btnCauta.Click += BtnCauta_Click;
        txtCauta.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnCauta_Click(s, e); };
        searchPanel.Controls.AddRange(new Control[] { txtCauta, btnCauta });

     
        tvwClienti.Dock          = DockStyle.Fill;
        tvwClienti.Font          = new Font("Segoe UI", 9);
        tvwClienti.ShowLines     = true;
        tvwClienti.ShowPlusMinus = true;
        tvwClienti.HideSelection = false;
        tvwClienti.AfterSelect  += TvwClienti_AfterSelect;

        leftPanel.Controls.Add(tvwClienti);
        leftPanel.Controls.Add(searchPanel);

    
        var rightPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };

        var lblDetalii = new Label { Text = "Detalii client", Dock = DockStyle.Top, Height = 28,
            Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(31, 56, 100),
            TextAlign = ContentAlignment.MiddleLeft };

        pgdClient.Dock        = DockStyle.Fill;
        pgdClient.PropertySort = PropertySort.Categorized;
        pgdClient.ToolbarVisible = true;

        rightPanel.Controls.Add(pgdClient);
        rightPanel.Controls.Add(lblDetalii);


        var bottomBar = new Panel { Dock = DockStyle.Bottom, Height = 52, BackColor = Color.FromArgb(31, 56, 100), Padding = new Padding(8, 8, 8, 8) };

        Button MkBtn(string t, Color c) { var b = new Button { Text = t, Height = 34, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(3,0,3,0) }; b.FlatAppearance.BorderSize = 0; return b; }

        btnAdauga   = MkBtn("➕ Adaugă",    Color.FromArgb(39, 174, 96));
        btnModifica = MkBtn("✏ Modifică",   Color.FromArgb(52, 152, 219));
        btnSterge   = MkBtn("🗑 Șterge",    Color.FromArgb(192, 57, 43));
        btnSalveaza = MkBtn("💾 Salvează în fișier", Color.FromArgb(142, 68, 173));
        btnSortAZ   = MkBtn("🔤 Sort A-Z",  Color.FromArgb(41, 128, 185));

        var btnFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        btnFlow.Controls.AddRange(new Control[] { btnAdauga, btnModifica, btnSterge, btnSalveaza, btnSortAZ });
        bottomBar.Controls.Add(btnFlow);

      
        var splitter = new Splitter { Dock = DockStyle.Left, Width = 4 };

        Controls.Add(rightPanel);
        Controls.Add(splitter);
        Controls.Add(leftPanel);
        Controls.Add(bottomBar);

        btnAdauga.Click   += BtnAdauga_Click;
        btnModifica.Click += BtnModifica_Click;
        btnSterge.Click   += BtnSterge_Click;
        btnSalveaza.Click += BtnSalveaza_Click;
        btnSortAZ.Click   += (s, e) => { clienti.Sort(new ClientNumeComparer()); RebuildTreeView(); };
    }

  

    private void IncarcaClienti()
    {
        clienti = db.Clienti.ToList();
        RebuildTreeView();
    }

    
    private void RebuildTreeView()
    {
        tvwClienti.BeginUpdate();
        tvwClienti.Nodes.Clear();

        foreach (TipClient tip in Enum.GetValues<TipClient>())
        {
            var parentNode = new TreeNode(tip == TipClient.PersoaneFizice ? "Persoane Fizice" : "Persoane Juridice")
            {
                Tag       = tip,
                ForeColor = Color.FromArgb(31, 56, 100),
                NodeFont  = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            foreach (var c in clienti.Where(cl => cl.TipClient == tip))
            {
                var childNode = new TreeNode(c.NumeComplet)
                {
                    Tag      = c,
                    ForeColor = Color.FromArgb(50, 50, 80)
                };
                parentNode.Nodes.Add(childNode);
            }

            tvwClienti.Nodes.Add(parentNode);
            parentNode.Expand();
        }

        tvwClienti.EndUpdate();
    }

  

    private void TvwClienti_AfterSelect(object? sender, TreeViewEventArgs e)
    {
       
        if (e.Node?.Level == 1 && e.Node.Tag is Client client)
            pgdClient.SelectedObject = client;
        else
            pgdClient.SelectedObject = null;
    }

  

    private void BtnCauta_Click(object? sender, EventArgs e)
    {
        string query = txtCauta.Text.Trim().ToLower();
        if (string.IsNullOrEmpty(query)) return;

        foreach (TreeNode parent in tvwClienti.Nodes)
            foreach (TreeNode child in parent.Nodes)
                if (child.Tag is Client c && c.NumeComplet.ToLower().Contains(query))
                {
                    tvwClienti.SelectedNode = child;
                    child.EnsureVisible();
                    return;
                }

        MessageBox.Show($"Nu s-a găsit niciun client cu '{query}'.", "Căutare", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    

    private void BtnAdauga_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmClient();
        if (frm.ShowDialog() == DialogResult.OK)
        {
            var client = new Client
            {
                Nume            = frm.Nume,
                Prenume         = frm.Prenume,
                CNP             = frm.CNP,
                Adresa          = frm.Adresa,
                Telefon         = frm.Telefon,
                Email           = frm.Email,
                PermisConducere = frm.PermisConducere,
                TipClient       = frm.TipClient
            };
            db.Clienti.Add(client);
            db.SaveChanges();
            IncarcaClienti();
            SelecteazaClientInTree(client);

            CreazaNotificareClientiNoi();
        }
    }

    private void BtnModifica_Click(object? sender, EventArgs e)
    {
        if (tvwClienti.SelectedNode?.Tag is not Client client)
        { MessageBox.Show("Selectați un client!", "Atenție"); return; }

        using var frm = new FrmClient(client);
        if (frm.ShowDialog() == DialogResult.OK)
        {
            client.Nume            = frm.Nume;
            client.Prenume         = frm.Prenume;
            client.CNP             = frm.CNP;
            client.Adresa          = frm.Adresa;
            client.Telefon         = frm.Telefon;
            client.Email           = frm.Email;
            client.PermisConducere = frm.PermisConducere;
            client.TipClient       = frm.TipClient;
            db.SaveChanges();
            IncarcaClienti();
        }
    }

    private void BtnSterge_Click(object? sender, EventArgs e)
    {
        if (tvwClienti.SelectedNode?.Tag is not Client client)
        { MessageBox.Show("Selectați un client!", "Atenție"); return; }

        var r = MessageBox.Show($"Ștergeți clientul '{client.NumeComplet}'?\nToate rezervările sale vor fi șterse!", "Confirmare",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (r == DialogResult.Yes)
        {
            db.Clienti.Remove(client);
            db.SaveChanges();
            pgdClient.SelectedObject = null;
            IncarcaClienti();
        }
    }

   
    private void BtnSalveaza_Click(object? sender, EventArgs e)
    {
        try
        {
            string cale = ReportHelper.SalveazaClienti(clienti);
            var r = MessageBox.Show($"Fișierul a fost salvat!\n{cale}\n\nDeschideți cu Notepad?", "Salvare reușită",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
                ReportHelper.DeschideInNotepad(cale);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SelecteazaClientInTree(Client client)
    {
        foreach (TreeNode parent in tvwClienti.Nodes)
            foreach (TreeNode child in parent.Nodes)
                if (child.Tag is Client c && c.Id == client.Id)
                {
                    tvwClienti.SelectedNode = child;
                    child.EnsureVisible();
                    return;
                }
    }

    private void CreazaNotificareClientiNoi()
    {
       
        var notif = new Label
        {
            Text      = "✓ Client nou adăugat cu succes!",
            Font      = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.DarkGreen,
            AutoSize  = true,
            Location  = new Point(8, 8)
        };
        
        var t = new System.Windows.Forms.Timer { Interval = 3000 };
        t.Tick += (s, e) => { Controls.Remove(notif); notif.Dispose(); t.Stop(); t.Dispose(); };
        Controls.Add(notif);
        notif.BringToFront();
        t.Start();
    }


}
