using System.ComponentModel;

namespace RentCar.Models;

public class Client
{
    public int Id { get; set; }

    [Description("Numele de familie"), Category("Date personale")]
    public string Nume { get; set; } = string.Empty;

    [Description("Prenumele"), Category("Date personale")]
    public string Prenume { get; set; } = string.Empty;

    [Description("Codul Numeric Personal"), Category("Date personale")]
    public string CNP { get; set; } = string.Empty;

    [Description("Adresa de domiciliu"), Category("Contact")]
    public string Adresa { get; set; } = string.Empty;

    [Description("Numarul de telefon"), Category("Contact")]
    public string Telefon { get; set; } = string.Empty;

    [Description("Adresa de email"), Category("Contact")]
    public string Email { get; set; } = string.Empty;

    [Description("Seria si numarul permisului de conducere"), Category("Documente")]
    public string PermisConducere { get; set; } = string.Empty;

    [Description("Tipul clientului"), Category("Clasificare")]
    public TipClient TipClient { get; set; } = TipClient.PersoaneFizice;

    [Description("Numarul total de rezervari"), Category("Statistici")]
    public int NrRezervari => Rezervari?.Count ?? 0;

    [Description("Numele complet"), Category("Date personale")]
    public string NumeComplet => $"{Prenume} {Nume}";

    public List<Rezervare> Rezervari { get; set; } = new();

    public override string ToString() => $"{NumeComplet} – {CNP}";
}
