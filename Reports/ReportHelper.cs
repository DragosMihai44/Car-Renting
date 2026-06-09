using RentCar.Models;
using System.Diagnostics;

namespace RentCar.Reports;

/// <summary>
/// Helper pentru generarea si salvarea rapoartelor in fisiere text (Lab 3 – StreamWriter).
/// </summary>
public static class ReportHelper
{
    public static string DirectorRapoarte =>
        Path.Combine(Application.StartupPath, "Rapoarte");

    public static void EnsureDirectory()
    {
        if (!Directory.Exists(DirectorRapoarte))
            Directory.CreateDirectory(DirectorRapoarte);
    }

    /// <summary>Salveaza lista de clienti sortata alfabetic (Lab 3).</summary>
    public static string SalveazaClienti(List<Client> clienti)
    {
        EnsureDirectory();
        string cale = Path.Combine(DirectorRapoarte, $"clienti_{DateTime.Now:yyyyMMdd_HHmm}.txt");
        var sortati = clienti.OrderBy(c => c.NumeComplet).ToList();

        using var sw = new StreamWriter(cale, false, System.Text.Encoding.UTF8);
        sw.WriteLine("======================================");
        sw.WriteLine("        LISTA CLIENTI – RENT CAR");
        sw.WriteLine($"        Generata la: {DateTime.Now:dd.MM.yyyy HH:mm}");
        sw.WriteLine("======================================");
        sw.WriteLine();
        foreach (var c in sortati)
        {
            sw.WriteLine($"Nume:    {c.NumeComplet}");
            sw.WriteLine($"CNP:     {c.CNP}");
            sw.WriteLine($"Telefon: {c.Telefon}");
            sw.WriteLine($"Email:   {c.Email}");
            sw.WriteLine($"Adresa:  {c.Adresa}");
            sw.WriteLine($"Tip:     {c.TipClient}");
            sw.WriteLine($"Permis:  {c.PermisConducere}");
            sw.WriteLine($"Rezervari: {c.NrRezervari}");
            sw.WriteLine(new string('-', 45));
        }
        return cale;
    }

    /// <summary>Salveaza raportul financiar pentru o perioada (Lab 3).</summary>
    public static string SalveazaRaportFinanciar(List<Rezervare> rezervari,
                                                   DateTime start, DateTime sfarsit,
                                                   string utilizator)
    {
        EnsureDirectory();
        string cale = Path.Combine(DirectorRapoarte, $"raport_financiar_{DateTime.Now:yyyyMMdd_HHmm}.txt");
        decimal total = rezervari.Sum(r => r.CostTotal);

        using var sw = new StreamWriter(cale, false, System.Text.Encoding.UTF8);
        sw.WriteLine("======================================");
        sw.WriteLine("      RAPORT FINANCIAR – RENT CAR");
        sw.WriteLine($"      Perioada: {start:dd.MM.yyyy} – {sfarsit:dd.MM.yyyy}");
        sw.WriteLine($"      Generat de: {utilizator}  |  {DateTime.Now:dd.MM.yyyy HH:mm}");
        sw.WriteLine("======================================");
        sw.WriteLine();
        sw.WriteLine($"{"Nr.",-5} {"Client",-25} {"Vehicul",-25} {"Zile",-6} {"Cost",-12} {"Stare",-12}");
        sw.WriteLine(new string('-', 90));
        int nr = 1;
        foreach (var r in rezervari.OrderBy(r => r.DataStart))
        {
            sw.WriteLine($"{nr,-5} {r.Client?.NumeComplet ?? "",-25} " +
                         $"{r.Vehicul?.Marca + " " + r.Vehicul?.Model,-25} " +
                         $"{r.NrZile,-6} {r.CostTotal,-12:C2} {r.Stare,-12}");
            nr++;
        }
        sw.WriteLine(new string('=', 90));
        sw.WriteLine($"{"TOTAL INCASARI:",-70} {total,12:C2}");
        sw.WriteLine($"{"NUMAR REZERVARI:",-70} {rezervari.Count,12}");
        return cale;
    }

    /// <summary>Deschide fisierul cu Notepad dupa salvare (Lab 3).</summary>
    public static void DeschideInNotepad(string cale)
    {
        if (File.Exists(cale))
            Process.Start("notepad.exe", cale);
    }
}
