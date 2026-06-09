using RentCar.Models;

namespace RentCar.Forms;


public class FrmClient : Form
{
    private TextBox txtNume           = new();
    private TextBox txtPrenume        = new();
    private TextBox txtCNP            = new();
    private TextBox txtAdresa         = new();
    private TextBox txtTelefon        = new();
    private TextBox txtEmail          = new();
    private TextBox txtPermis         = new();
    private ComboBox cmbTipClient     = new();
    private Button btnOk              = new();
    private Button btnCancel          = new();
    private ErrorProvider errorProvider = new();

    public string    Nume            => txtNume.Text.Trim();
    public string    Prenume         => txtPrenume.Text.Trim();
    public string    CNP             => txtCNP.Text.Trim();
    public string    Adresa          => txtAdresa.Text.Trim();
    public string    Telefon         => txtTelefon.Text.Trim();
    public string    Email           => txtEmail.Text.Trim();
    public string    PermisConducere => txtPermis.Text.Trim();
    public TipClient TipClient       => (TipClient)cmbTipClient.SelectedIndex;

    public FrmClient(Client? clientExistent = null)
    {
        InitializeComponents();
        if (clientExistent != null)
            PrecompletaDate(clientExistent);
    }

    private void InitializeComponents()
    {
        Text            = "Client";
        Size            = new Size(430, 430);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = Color.FromArgb(245, 248, 252);

        int lX = 20, cX = 145, cW = 250, rH = 36, sY = 15;

        void AddRow(string lbl, Control ctrl, int row)
        {
            var l = new Label { Text = lbl, Location = new Point(lX, sY + row * rH + 4),
                                Size = new Size(120, 22), Font = new Font("Segoe UI", 9),
                                TextAlign = ContentAlignment.MiddleRight };
            ctrl.Location = new Point(cX, sY + row * rH);
            ctrl.Size     = new Size(cW, 26);
            ctrl.Font     = new Font("Segoe UI", 9);
            Controls.Add(l);
            Controls.Add(ctrl);
        }

        cmbTipClient.Items.AddRange(new[] { "Persoana Fizică", "Persoana Juridică" });
        cmbTipClient.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTipClient.SelectedIndex = 0;

        AddRow("Nume *:",           txtNume,       0);
        AddRow("Prenume *:",        txtPrenume,    1);
        AddRow("CNP *:",            txtCNP,        2);
        AddRow("Adresă:",           txtAdresa,     3);
        AddRow("Telefon *:",        txtTelefon,    4);
        AddRow("Email:",            txtEmail,      5);
        AddRow("Permis conducere:", txtPermis,     6);
        AddRow("Tip client:",       cmbTipClient,  7);

        btnOk.Text      = "OK";
        btnOk.Location  = new Point(145, sY + 8 * rH + 8);
        btnOk.Size      = new Size(115, 34);
        btnOk.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        btnOk.BackColor = Color.FromArgb(46, 117, 182);
        btnOk.ForeColor = Color.White;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.Click    += BtnOk_Click;

        btnCancel.Text         = "Anulează";
        btnCancel.Location     = new Point(270, sY + 8 * rH + 8);
        btnCancel.Size         = new Size(115, 34);
        btnCancel.Font         = new Font("Segoe UI", 10);
        btnCancel.FlatStyle    = FlatStyle.Flat;
        btnCancel.DialogResult = DialogResult.Cancel;

        AcceptButton = btnOk;
        CancelButton = btnCancel;
        Controls.AddRange(new Control[] { btnOk, btnCancel });
        errorProvider.ContainerControl = this;
    }

    private void PrecompletaDate(Client c)
    {
        Text                = "Modifică client";
        txtNume.Text        = c.Nume;
        txtPrenume.Text     = c.Prenume;
        txtCNP.Text         = c.CNP;
        txtAdresa.Text      = c.Adresa;
        txtTelefon.Text     = c.Telefon;
        txtEmail.Text       = c.Email;
        txtPermis.Text      = c.PermisConducere;
        cmbTipClient.SelectedIndex = (int)c.TipClient;
    }

    private void BtnOk_Click(object? sender, EventArgs e)
    {
        errorProvider.Clear();
        bool valid = true;

        if (string.IsNullOrWhiteSpace(txtNume.Text))
        { errorProvider.SetError(txtNume, "Numele este obligatoriu!"); valid = false; }

        if (string.IsNullOrWhiteSpace(txtPrenume.Text))
        { errorProvider.SetError(txtPrenume, "Prenumele este obligatoriu!"); valid = false; }

        if (string.IsNullOrWhiteSpace(txtCNP.Text) || txtCNP.Text.Length != 13)
        { errorProvider.SetError(txtCNP, "CNP-ul trebuie să aibă 13 cifre!"); valid = false; }

        if (string.IsNullOrWhiteSpace(txtTelefon.Text))
        { errorProvider.SetError(txtTelefon, "Telefonul este obligatoriu!"); valid = false; }

        if (valid)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
