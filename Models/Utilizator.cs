using System.ComponentModel;

namespace RentCar.Models;

public class Utilizator
{
    public int Id { get; set; }

    [Description("Numele de utilizator"), Category("Cont")]
    public string NumeUtilizator { get; set; } = string.Empty;

    public string ParolaHash { get; set; } = string.Empty;

    [Description("Rolul utilizatorului în sistem"), Category("Cont")]
    public RolUtilizator Rol { get; set; } = RolUtilizator.Agent;

    [Description("Dacă contul este activ"), Category("Cont")]
    public bool Activ { get; set; } = true;

    public override string ToString() => $"{NumeUtilizator} ({Rol})";

    public static string HashParola(string parola)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(parola);
        var hash  = sha.ComputeHash(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
