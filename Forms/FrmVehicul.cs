using RentCar.Models;

namespace RentCar.Forms
{
    public partial class FrmVehicul : Form
    {
        public string   Marca            => txtMarca.Text.Trim();
        public string   Model            => txtModel.Text.Trim();
        public string   NrInmatriculare  => txtNrInmatriculare.Text.Trim().ToUpper();
        public int      AnFabricatie     => (int)nudAn.Value;
        public decimal  TarifZiLei       => nudTarif.Value;
        public int      Kilometraj       => (int)nudKm.Value;
        public int      CategorieId      => (int)(cmbCategorie.SelectedValue ?? 1);
        public string   Stare            => StareVehiculExtensions.FromDisplay(cmbStare.Text).ToString();
        public DateTime DataReviziei     => dtpReviziei.Value;
        public DateTime DataITP          => dtpITP.Value;

        private readonly List<CategorieVehicul> categorii;

        public FrmVehicul(List<CategorieVehicul> categorii, Vehicul? vehiculExistent = null)
        {
            this.categorii = categorii;
            InitializeComponent();
            PopuleazaCategorii();
            if (vehiculExistent != null)
                PrecompletaDate(vehiculExistent);
        }

        private void PopuleazaCategorii()
        {
            cmbCategorie.DataSource    = categorii;
            cmbCategorie.DisplayMember = "Denumire";
            cmbCategorie.ValueMember   = "Id";
            cmbCategorie.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PrecompletaDate(Vehicul v)
        {
            Text = "Modifică vehicul";
            txtMarca.Text           = v.Marca;
            txtModel.Text           = v.Model;
            txtNrInmatriculare.Text  = v.NrInmatriculare;
            nudAn.Value             = v.AnFabricatie;
            nudTarif.Value          = v.TarifZiLei;
            nudKm.Value             = v.Kilometraj;
            cmbStare.Text           = v.Stare.ToDisplay();
            dtpReviziei.Value       = v.DataReviziei;
            dtpITP.Value            = v.DataITP;
            var cat = categorii.FirstOrDefault(c => c.Id == v.CategorieId);
            if (cat != null) cmbCategorie.SelectedItem = cat;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            { errorProvider.SetError(txtMarca, "Marca este obligatorie!"); valid = false; }
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            { errorProvider.SetError(txtModel, "Modelul este obligatoriu!"); valid = false; }
            if (string.IsNullOrWhiteSpace(txtNrInmatriculare.Text))
            { errorProvider.SetError(txtNrInmatriculare, "Nr. înmatriculare obligatoriu!"); valid = false; }
            if (valid) { DialogResult = DialogResult.OK; Close(); }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
