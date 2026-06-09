using System.ComponentModel;

namespace RentCar.Models;

public class Rezervare
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int VehiculId { get; set; }
    public Vehicul Vehicul { get; set; } = null!;

    [Description("Data inceperii inchirierii"), Category("Perioada")]
    public DateTime DataStart { get; set; } = DateTime.Today;

    [Description("Data returnarii planificate"), Category("Perioada")]
    public DateTime DataRetur { get; set; } = DateTime.Today.AddDays(1);

    [Description("Costul total calculat (lei)"), Category("Financiar")]
    public decimal CostTotal { get; set; }

    [Description("Starea rezervarii"), Category("General")]
    public StareRezervare Stare { get; set; } = StareRezervare.Activa;

    [Description("Locatia de preluare"), Category("Logistica")]
    public string LocatiePreluare { get; set; } = string.Empty;

    [Description("Locatia de returnare"), Category("Logistica")]
    public string LocatieReturnare { get; set; } = string.Empty;

    public int NrZile => Math.Max(1, (int)(DataRetur - DataStart).TotalDays);

    public Contract? Contract { get; set; }
    public Returnare? Returnare { get; set; }

    public override string ToString() =>
        $"#{Id} | {Vehicul?.Marca} {Vehicul?.Model} | {DataStart:dd.MM.yyyy} – {DataRetur:dd.MM.yyyy} | {CostTotal:C2} | {Stare}";
}

public class Contract
{
    public int Id { get; set; }
    public int RezervareId { get; set; }
    public Rezervare Rezervare { get; set; } = null!;

    [Description("Data emiterii contractului"), Category("General")]
    public DateTime DataEmitere { get; set; } = DateTime.Today;

    [Description("Depozitul de garantie (lei)"), Category("Financiar")]
    public decimal DepozitGarantie { get; set; }

    [Description("Contractul a fost semnat"), Category("General")]
    public bool Semnat { get; set; }

    public override string ToString() =>
        $"Contract #{Id} – {DataEmitere:dd.MM.yyyy} – Garantie: {DepozitGarantie:C2}";
}

public class Returnare
{
    public int Id { get; set; }
    public int RezervareId { get; set; }
    public Rezervare Rezervare { get; set; } = null!;

    [Description("Data efectiva a returnarii"), Category("General")]
    public DateTime DataEfectiva { get; set; } = DateTime.Today;

    [Description("Kilometrajul la returnare"), Category("Tehnic")]
    public int KmFinal { get; set; }

    [Description("Observatii la returnare"), Category("General")]
    public string Observatii { get; set; } = string.Empty;

    [Description("Penalizare pentru intarziere (lei)"), Category("Financiar")]
    public decimal Penalizare { get; set; }

    public override string ToString() =>
        $"Returnare {DataEfectiva:dd.MM.yyyy} – Km: {KmFinal} – Penalizare: {Penalizare:C2}";
}
