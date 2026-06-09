using System.ComponentModel;

namespace RentCar.Models;

public class Vehicul
{
    public int Id { get; set; }

    [Description("Marca vehiculului"), Category("Identificare")]
    public string Marca { get; set; } = string.Empty;

    [Description("Modelul vehiculului"), Category("Identificare")]
    public string Model { get; set; } = string.Empty;

    [Description("Anul fabricatiei"), Category("Identificare")]
    public int AnFabricatie { get; set; }

    [Description("Numarul de inmatriculare"), Category("Identificare")]
    public string NrInmatriculare { get; set; } = string.Empty;

    public int CategorieId { get; set; }

    [Description("Categoria vehiculului"), Category("Clasificare")]
    public CategorieVehicul? Categorie { get; set; }

    [Description("Tariful pe zi in lei"), Category("Financiar")]
    public decimal TarifZiLei { get; set; }

    [Description("Kilometrajul curent"), Category("Tehnic")]
    public int Kilometraj { get; set; }

    [Description("Starea curenta a vehiculului"), Category("Stare")]
    public StareVehicul Stare { get; set; } = StareVehicul.Disponibil;

    [Description("Data adaugarii in flota"), Category("General")]
    public DateTime DataAdaugare { get; set; } = DateTime.Today;

    [Description("Data urmatoarei revizii"), Category("Tehnic")]
    public DateTime DataReviziei { get; set; } = DateTime.Today.AddMonths(6);

    [Description("Data expirarii ITP"), Category("Tehnic")]
    public DateTime DataITP { get; set; } = DateTime.Today.AddYears(1);

    public bool ITPExpirat => DataITP < DateTime.Today;
    public bool RevizieDepasita => DataReviziei < DateTime.Today;

    public List<Rezervare> Rezervari { get; set; } = new();

    public override string ToString() =>
        $"{Marca} {Model} ({AnFabricatie}) – {NrInmatriculare} [{Stare}]";
}
