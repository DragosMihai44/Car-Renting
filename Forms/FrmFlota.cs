using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Forms
{
    public partial class FrmFlota : Form
    {
        private readonly RentCarDbContext db = new();
        private List<CategorieVehicul> categorii = new();
        private List<Vehicul> vehicule = new();
        private bool sortTarifAsc = true;

        public FrmFlota()
        {
            InitializeComponent();
            LoadCategorii();
            LoadVehicule();
            AplicaFiltru();
        }

        private void LoadCategorii()
        {
            categorii = db.CategoriiVehicule.ToList();
            var items = new List<object> { "(Toate)" };
            items.AddRange(categorii.Cast<object>());
            cmbFiltruCategorie.DataSource    = items;
            cmbFiltruCategorie.DisplayMember = "Denumire";
            cmbFiltruCategorie.SelectedIndex = 0;
        }

        private void LoadVehicule() =>
            vehicule = db.Vehicule.Include(v => v.Categorie).ToList();

     
        private void AplicaFiltru()
        {
            var filtered = vehicule.AsEnumerable();
            if (cmbFiltruCategorie.SelectedItem is CategorieVehicul cat)
                filtered = filtered.Where(v => v.CategorieId == cat.Id);
            if (cmbFiltruStare.SelectedIndex > 0)
            {
                var stareText = cmbFiltruStare.SelectedItem?.ToString();
                if (stareText != null)
                {
                    var st = StareVehiculExtensions.FromDisplay(stareText);
                    filtered = filtered.Where(v => v.Stare == st);
                }
            }

            BindGrid(filtered.ToList());
        }

        private void BindGrid(List<Vehicul> lista)
        {
            dgvVehicule.DataSource = lista.Select(v => new
            {
                v.Id, v.Marca, v.Model, v.AnFabricatie, v.NrInmatriculare,
                Categorie  = v.Categorie?.Denumire ?? "",
                v.TarifZiLei, v.Kilometraj,
                Stare      = v.Stare.ToDisplay()
            }).ToList();
            lblTotal.Text = $"Total: {dgvVehicule.Rows.Count} vehicule";
        }

        private int? GetSelectedId()
        {
            var item = dgvVehicule.CurrentRow?.DataBoundItem;
            return (int?)item?.GetType().GetProperty("Id")?.GetValue(item);
        }

        
        private void CmbFiltru_SelectedIndexChanged(object sender, EventArgs e) => AplicaFiltru();

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmbFiltruCategorie.SelectedIndex = 0;
            cmbFiltruStare.SelectedIndex = 0;
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            using var frm = new FrmVehicul(categorii);
            if (frm.ShowDialog() != DialogResult.OK) return;
            try
            {
                db.Vehicule.Add(new Vehicul
                {
                    Marca = frm.Marca, Model = frm.Model, AnFabricatie = frm.AnFabricatie,
                    NrInmatriculare = frm.NrInmatriculare, CategorieId = frm.CategorieId,
                    TarifZiLei = frm.TarifZiLei, Kilometraj = frm.Kilometraj,
                    Stare = StareVehicul.Disponibil,
                    DataAdaugare = DateTime.Now, DataReviziei = DateTime.Now.AddMonths(6), DataITP = DateTime.Now.AddYears(1)
                });
                db.SaveChanges(); LoadVehicule(); AplicaFiltru();
            }
            catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnModifica_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedId();
            if (id == null) { MessageBox.Show("Selectați un vehicul!"); return; }
            var v = db.Vehicule.Include(x => x.Categorie).FirstOrDefault(x => x.Id == id);
            if (v == null) return;
            using var frm = new FrmVehicul(categorii, v);
            if (frm.ShowDialog() != DialogResult.OK) return;
            try
            {
                v.Marca = frm.Marca; v.Model = frm.Model; v.AnFabricatie = frm.AnFabricatie;
                v.NrInmatriculare = frm.NrInmatriculare; v.CategorieId = frm.CategorieId;
                v.TarifZiLei = frm.TarifZiLei; v.Kilometraj = frm.Kilometraj;
                if (Enum.TryParse<StareVehicul>(frm.Stare, out var st)) v.Stare = st;
                db.SaveChanges(); LoadVehicule(); AplicaFiltru();
            }
            catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedId();
            if (id == null) { MessageBox.Show("Selectați un vehicul!"); return; }
            var v = db.Vehicule.Find(id); if (v == null) return;
            if (MessageBox.Show($"Ștergeți '{v.Marca} {v.Model}'?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try { db.Vehicule.Remove(v); db.SaveChanges(); LoadVehicule(); AplicaFiltru(); }
                catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BtnActualizeazaStare_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedId();
            if (id == null) { MessageBox.Show("Selectați un vehicul!"); return; }
            using var dlg = new Form { Text = "Actualizează stare", Size = new Size(280, 150), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
            var cmb = new ComboBox { Location = new Point(20, 20), Size = new Size(220, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(new[] { "Disponibil", "Închiriat", "In service" }); cmb.SelectedIndex = 0;
            var btn = new Button { Text = "OK", Location = new Point(20, 60), Size = new Size(100, 34), DialogResult = DialogResult.OK };
            dlg.Controls.AddRange(new Control[] { cmb, btn }); dlg.AcceptButton = btn;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var v = db.Vehicule.Find(id);
                if (v != null)
                {
                    v.Stare = StareVehiculExtensions.FromDisplay(cmb.SelectedItem!.ToString()!);
                    db.SaveChanges(); LoadVehicule(); AplicaFiltru();
                }
            }
        }

        private void BtnSortTarif_Click(object sender, EventArgs e)
        {
            var sorted = sortTarifAsc ? vehicule.OrderBy(v => v.TarifZiLei).ToList() : vehicule.OrderByDescending(v => v.TarifZiLei).ToList();
            sortTarifAsc = !sortTarifAsc;
            btnSortTarif.Text = sortTarifAsc ? "💰 Sort Tarif ▲" : "💰 Sort Tarif ▼";
            BindGrid(sorted);
        }

        private void BtnSortKm_Click(object sender, EventArgs e) =>
            BindGrid(vehicule.OrderBy(v => v.Kilometraj).ToList());
    }
}
