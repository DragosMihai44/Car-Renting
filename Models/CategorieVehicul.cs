using System.ComponentModel;

namespace RentCar.Models;

public class CategorieVehicul
{
    public int Id { get; set; }

    [Description("Denumirea categoriei"), Category("General")]
    public string Denumire { get; set; } = string.Empty;

    [Description("Tariful minim pe zi (lei)"), Category("Tarife")]
    public decimal TarifMinim { get; set; }

    [Description("Tariful maxim pe zi (lei)"), Category("Tarife")]
    public decimal TarifMaxim { get; set; }

    public List<Vehicul> Vehicule { get; set; } = new();

    public override string ToString() => Denumire;
}
