namespace RentCar.Models;

public enum StareVehicul
{
    Disponibil,
    Inchiriat,
    InService
}

public enum TipClient
{
    PersoaneFizice,
    PersoaneJuridice
}

public enum StareRezervare
{
    Activa,
    Finalizata,
    Anulata
}

public enum RolUtilizator
{
    Administrator,
    Agent,
    Mecanic
}
