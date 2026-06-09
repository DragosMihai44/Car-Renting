using RentCar.Data;
using RentCar.Models;

namespace RentCar.Forms
{
    public partial class FrmPrincipal : Form
    {
        private readonly Utilizator utilizator;
        private readonly RentCarDbContext db = new();
        private readonly List<Form> formsEmbedded = new();

        public FrmPrincipal(Utilizator utilizator)
        {
            this.utilizator = utilizator;
            InitializeComponent();
            Text = $"Rent Car – {utilizator.NumeUtilizator} ({utilizator.Rol})";
            lblUser.Text = $"Utilizator: {utilizator.NumeUtilizator} | {utilizator.Rol}";
            BuildTabs();
            VerificaAlerteVehicule();
            timerCeas.Start();
        }

        private void BuildTabs()
        {
            var tabDash = new TabPage("  Dashboard");
            tabDash.Controls.Add(BuildDashboard());
            tabMain.TabPages.Add(tabDash);

            if (utilizator.Rol != RolUtilizator.Mecanic)
                tabMain.TabPages.Add(CreazaTab("  Flotă",      () => new FrmFlota()));
            if (utilizator.Rol != RolUtilizator.Mecanic)
                tabMain.TabPages.Add(CreazaTab("  Clienți",    () => new FrmClienti(new RentCarDbContext())));
            if (utilizator.Rol != RolUtilizator.Mecanic)
                tabMain.TabPages.Add(CreazaTab("  Rezervări",  () => new FrmRezervari(new RentCarDbContext(), utilizator)));

            tabMain.TabPages.Add(CreazaTab("  Service",    () => new FrmService(new RentCarDbContext())));

            if (utilizator.Rol != RolUtilizator.Mecanic)
                tabMain.TabPages.Add(CreazaTab("  Rapoarte",   () => new FrmRapoarte(new RentCarDbContext(), utilizator)));
        }

        private TabPage CreazaTab(string titlu, Func<Form> factory)
        {
            var tab = new TabPage(titlu);
            bool loaded = false;
            tabMain.Selected += (s, e) =>
            {
                if (e.TabPage != tab || loaded) return;
                loaded = true;
                Form frm = factory();
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Visible = true;
                frm.FormClosing += (fs, fe) => { if (fe.CloseReason == CloseReason.UserClosing) fe.Cancel = true; };
                tab.Controls.Add(frm);
                formsEmbedded.Add(frm);
            };
            return tab;
        }

        private Control BuildDashboard()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24), BackColor = Color.FromArgb(245, 248, 252) };
            panel.Controls.Add(new Label { Text = $"Bun venit, {utilizator.NumeUtilizator}!", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(31, 56, 100), AutoSize = true, Location = new Point(24, 24) });
            panel.Controls.Add(new Label { Text = $"Rol: {utilizator.Rol}  |  {DateTime.Now:dddd, dd MMMM yyyy}", Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.Gray, AutoSize = true, Location = new Point(24, 62) });

            int cardW = 200, cardH = 100, gap = 20, startX = 24, startY = 110;
            void AddCard(string title, string value, Color color, int col)
            {
                var card = new Panel { Location = new Point(startX + col * (cardW + gap), startY), Size = new Size(cardW, cardH), BackColor = color };
                card.Controls.Add(new Label { Text = title, Location = new Point(12, 12), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(220, 240, 255) });
                card.Controls.Add(new Label { Text = value, Location = new Point(12, 36), AutoSize = true, Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.White });
                panel.Controls.Add(card);
            }
            try
            {
                AddCard("Vehicule disponibile", db.Vehicule.Count(v => v.Stare == StareVehicul.Disponibil).ToString(), Color.FromArgb(39, 174, 96), 0);
                AddCard("Vehicule închiriate",  db.Vehicule.Count(v => v.Stare == StareVehicul.Inchiriat).ToString(),  Color.FromArgb(52, 152, 219), 1);
                AddCard("Total clienți",        db.Clienti.Count().ToString(),                                          Color.FromArgb(142, 68, 173), 2);
                AddCard("Rezervări active",     db.Rezervari.Count(r => r.Stare == StareRezervare.Activa).ToString(),  Color.FromArgb(230, 126, 34), 3);
            }
            catch { }
            return panel;
        }

        private void VerificaAlerteVehicule()
        {
            try
            {
                int alerte = db.Vehicule.Count(v => v.DataITP < DateTime.Today || v.DataReviziei < DateTime.Today);
                lblStatus.Text = alerte > 0 ? $"ATENȚIE: {alerte} vehicule cu alerte de service!" : "Toate vehiculele sunt în regulă.";
            }
            catch { lblStatus.Text = "Rent Car gata."; }
        }

        private void TimerCeas_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void MnuIesire_Click(object sender, EventArgs e) => Application.Exit();

        private void MnuDespre_Click(object sender, EventArgs e) =>
            MessageBox.Show("Rent Car v1.0\nMSOA – Universitatea Politehnica Timișoara", "Despre", MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerCeas.Stop();
            foreach (var frm in formsEmbedded)
                try { frm.Dispose(); } catch { }
            db.Dispose();
            base.OnFormClosing(e);
        }
    }
}
