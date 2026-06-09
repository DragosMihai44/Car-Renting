namespace RentCar.Models
{
    public static class StareVehiculExtensions
    {
        
        public static string ToDisplay(this StareVehicul stare) => stare switch
        {
            StareVehicul.Disponibil => "Disponibil",
            StareVehicul.Inchiriat  => "Închiriat",
            StareVehicul.InService  => "In service",
            _                       => stare.ToString()
        };

        
        public static StareVehicul FromDisplay(string text) => text switch
        {
            "Disponibil" => StareVehicul.Disponibil,
            "Închiriat"  => StareVehicul.Inchiriat,
            "In service" => StareVehicul.InService,
            _            => Enum.Parse<StareVehicul>(text)
        };
    }
}
