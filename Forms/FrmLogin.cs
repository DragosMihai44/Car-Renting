using RentCar.Data;
using RentCar.Models;

namespace RentCar.Forms
{

    public partial class FrmLogin : Form
    {
        public Utilizator? UtilizatorAutentificat { get; private set; }
        private readonly RentCarDbContext db = new();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnAutentificare_Click(object sender, EventArgs e)
        {
            string user = txtUtilizator.Text.Trim();
            string hash = Utilizator.HashParola(txtParola.Text);

            var utilizator = db.Utilizatori.FirstOrDefault(u =>
                u.NumeUtilizator == user && u.ParolaHash == hash && u.Activ);

            if (utilizator == null)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Utilizator sau parolă incorectă!";
                txtParola.Clear();
                txtParola.Focus();
                return;
            }

            UtilizatorAutentificat = utilizator;
            btnAutentificare.Enabled = false;
            btnIesire.Enabled = false;
            txtUtilizator.Enabled = false;
            txtParola.Enabled = false;
            lblStatus.ForeColor = Color.FromArgb(46, 117, 182);
            lblStatus.Text = $"Bun venit, {utilizator.NumeUtilizator}! Se inițializează...";
            progressBar.Value = 0;
            timer.Start();
        }

        private void BtnIesire_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBar.Value++;
            if (progressBar.Value >= progressBar.Maximum)
            {
                timer.Stop();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            db.Dispose();
            base.OnFormClosing(e);
        }
    }
}
