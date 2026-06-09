using RentCar.Models;

namespace RentCar.Comparers;

public class TarifComparer : IComparer<Vehicul>
{
    public int Compare(Vehicul? x, Vehicul? y)
    {
        if (x == null || y == null) return 0;
        return x.TarifZiLei.CompareTo(y.TarifZiLei);
    }
}


public class KilometrajComparer : IComparer<Vehicul>
{
    public int Compare(Vehicul? x, Vehicul? y)
    {
        if (x == null || y == null) return 0;
        return x.Kilometraj.CompareTo(y.Kilometraj);
    }
}


public class DataAdaugareComparer : IComparer<Vehicul>
{
    public int Compare(Vehicul? x, Vehicul? y)
    {
        if (x == null || y == null) return 0;
        return x.DataAdaugare.CompareTo(y.DataAdaugare);
    }
}

public class ClientNumeComparer : IComparer<Client>
{
    public int Compare(Client? x, Client? y)
    {
        if (x == null || y == null) return 0;
        return string.Compare(x.NumeComplet, y.NumeComplet, StringComparison.OrdinalIgnoreCase);
    }
}
