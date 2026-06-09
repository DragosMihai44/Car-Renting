using RentCar.Data;
using RentCar.Forms;
using RentCar.Models;

namespace RentCar;


static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            using var db = new RentCarDbContext();
            db.Database.EnsureCreated();

          
            if (!db.CategoriiVehicule.Any())
            {
                db.CategoriiVehicule.AddRange(new[]
                {
                    new CategorieVehicul { Denumire = "Mica",      TarifMinim = 80,  TarifMaxim = 120 },
                    new CategorieVehicul { Denumire = "Compacta",  TarifMinim = 120, TarifMaxim = 180 },
                    new CategorieVehicul { Denumire = "SUV",       TarifMinim = 200, TarifMaxim = 350 },
                    new CategorieVehicul { Denumire = "Premium",   TarifMinim = 350, TarifMaxim = 600 },
                    new CategorieVehicul { Denumire = "Utilitara", TarifMinim = 150, TarifMaxim = 250 },
                });
                db.SaveChanges();
            }

         
            if (!db.Vehicule.Any())
            {
                var mica     = db.CategoriiVehicule.First(c => c.Denumire == "Mica");
                var compacta = db.CategoriiVehicule.First(c => c.Denumire == "Compacta");
                var suv      = db.CategoriiVehicule.First(c => c.Denumire == "SUV");
                var premium  = db.CategoriiVehicule.First(c => c.Denumire == "Premium");
                var util     = db.CategoriiVehicule.First(c => c.Denumire == "Utilitara");
                db.Vehicule.AddRange(new[]
                {
                    new Vehicul { Marca="Dacia",      Model="Logan",   AnFabricatie=2021, NrInmatriculare="TM-01-ABC", CategorieId=mica.Id,     TarifZiLei=95,  Kilometraj=45000, Stare=StareVehicul.Disponibil },
                    new Vehicul { Marca="Skoda",      Model="Octavia", AnFabricatie=2022, NrInmatriculare="TM-02-DEF", CategorieId=compacta.Id, TarifZiLei=150, Kilometraj=30000, Stare=StareVehicul.Disponibil },
                    new Vehicul { Marca="BMW",        Model="X5",      AnFabricatie=2023, NrInmatriculare="TM-03-GHI", CategorieId=suv.Id,      TarifZiLei=280, Kilometraj=15000, Stare=StareVehicul.Disponibil },
                    new Vehicul { Marca="Mercedes",   Model="Clasa C", AnFabricatie=2023, NrInmatriculare="TM-04-JKL", CategorieId=premium.Id,  TarifZiLei=450, Kilometraj=12000, Stare=StareVehicul.Inchiriat  },
                    new Vehicul { Marca="Renault",    Model="Kangoo",  AnFabricatie=2020, NrInmatriculare="TM-05-MNO", CategorieId=util.Id,     TarifZiLei=180, Kilometraj=80000, Stare=StareVehicul.Disponibil },
                    new Vehicul { Marca="Volkswagen", Model="Golf",    AnFabricatie=2022, NrInmatriculare="TM-06-PQR", CategorieId=compacta.Id, TarifZiLei=140, Kilometraj=25000, Stare=StareVehicul.InService,
                        DataITP=DateTime.Today.AddMonths(-2), DataReviziei=DateTime.Today.AddMonths(-1) },
                    new Vehicul { Marca="Toyota",     Model="Corolla", AnFabricatie=2021, NrInmatriculare="TM-07-STU", CategorieId=compacta.Id, TarifZiLei=130, Kilometraj=35000, Stare=StareVehicul.Disponibil,
                        DataReviziei=DateTime.Today.AddDays(-5) },
                    new Vehicul { Marca="Audi",       Model="A6",      AnFabricatie=2022, NrInmatriculare="TM-08-VWX", CategorieId=premium.Id,  TarifZiLei=400, Kilometraj=18000, Stare=StareVehicul.Disponibil,
                        DataReviziei=DateTime.Today.AddDays(20) },
                });
                db.SaveChanges();
            }

         
            if (!db.Clienti.Any())
            {
                db.Clienti.AddRange(new[]
                {
                    new Client { Nume="Popescu",    Prenume="Ion",       CNP="1900101123456", Adresa="Timisoara, str. Florilor 10",    Telefon="0721111222", Email="ion.popescu@email.ro",     PermisConducere="TM123456", TipClient=TipClient.PersoaneFizice   },
                    new Client { Nume="Ionescu",    Prenume="Maria",     CNP="2850615234567", Adresa="Timisoara, bd. Revolutiei 5",    Telefon="0732222333", Email="maria.ionescu@email.ro",   PermisConducere="TM234567", TipClient=TipClient.PersoaneFizice   },
                    new Client { Nume="Constantin", Prenume="Alexandru", CNP="1780320345678", Adresa="Arad, str. Victoriei 22",        Telefon="0743333444", Email="alex.constantin@firma.ro", PermisConducere="AR345678", TipClient=TipClient.PersoaneJuridice },
                    new Client { Nume="Gheorghe",   Prenume="Elena",     CNP="2920430456789", Adresa="Lugoj, str. Mihai Viteazu 3",   Telefon="0754444555", Email="elena.gheorghe@email.ro",  PermisConducere="TM456789", TipClient=TipClient.PersoaneFizice   },
                });
                db.SaveChanges();
            }

           
            if (!db.Rezervari.Any() && db.Vehicule.Any() && db.Clienti.Any())
            {
                var clienti  = db.Clienti.ToList();
                var vehicule = db.Vehicule.ToList();
                db.Rezervari.AddRange(new[]
                {
                    new Rezervare { ClientId=clienti[0].Id, VehiculId=vehicule[0].Id, DataStart=DateTime.Today.AddDays(-30), DataRetur=DateTime.Today.AddDays(-23), CostTotal=vehicule[0].TarifZiLei*7, Stare=StareRezervare.Finalizata, LocatiePreluare="Timisoara", LocatieReturnare="Timisoara" },
                    new Rezervare { ClientId=clienti[1].Id, VehiculId=vehicule[1].Id, DataStart=DateTime.Today.AddDays(-20), DataRetur=DateTime.Today.AddDays(-15), CostTotal=vehicule[1].TarifZiLei*5, Stare=StareRezervare.Finalizata, LocatiePreluare="Timisoara", LocatieReturnare="Arad"      },
                    new Rezervare { ClientId=clienti[2].Id, VehiculId=vehicule[2].Id, DataStart=DateTime.Today.AddDays(-10), DataRetur=DateTime.Today.AddDays(-7),  CostTotal=vehicule[2].TarifZiLei*3, Stare=StareRezervare.Finalizata, LocatiePreluare="Timisoara", LocatieReturnare="Timisoara" },
                    new Rezervare { ClientId=clienti[3%clienti.Count].Id, VehiculId=vehicule[3].Id, DataStart=DateTime.Today.AddDays(-5), DataRetur=DateTime.Today.AddDays(2), CostTotal=vehicule[3].TarifZiLei*7, Stare=StareRezervare.Activa, LocatiePreluare="Timisoara", LocatieReturnare="Timisoara" },
                    new Rezervare { ClientId=clienti[0].Id, VehiculId=vehicule[1].Id, DataStart=DateTime.Today.AddDays(-60), DataRetur=DateTime.Today.AddDays(-55), CostTotal=vehicule[1].TarifZiLei*5, Stare=StareRezervare.Finalizata, LocatiePreluare="Arad",      LocatieReturnare="Arad"      },
                    new Rezervare { ClientId=clienti[1].Id, VehiculId=vehicule[0].Id, DataStart=DateTime.Today.AddDays(-45), DataRetur=DateTime.Today.AddDays(-42), CostTotal=vehicule[0].TarifZiLei*3, Stare=StareRezervare.Anulata,   LocatiePreluare="Timisoara", LocatieReturnare="Timisoara" },
                });
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Eroare initializare baza de date:\n" + ex.Message,
                "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

      
        using var loginFrm = new FrmLogin();
        if (loginFrm.ShowDialog() != DialogResult.OK || loginFrm.UtilizatorAutentificat == null)
            return;  

        
        Application.Run(new FrmPrincipal(loginFrm.UtilizatorAutentificat));
    }
}
