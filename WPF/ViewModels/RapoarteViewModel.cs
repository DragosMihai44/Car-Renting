using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RentCarWPF.Commands;

namespace RentCarWPF.ViewModels;

/// <summary>
/// Model de date pentru o rezervare afisata in WPF (Lab 9 – INotifyPropertyChanged).
/// </summary>
public class RezervareModel : INotifyPropertyChanged
{
    private string client    = string.Empty;
    private string vehicul   = string.Empty;
    private DateTime dataStart;
    private DateTime dataRetur;
    private decimal costTotal;
    private string stare     = string.Empty;
    private double rating    = 5.0;

    public int Id { get; set; }

    public string Client
    {
        get => client;
        set { client = value; OnPropertyChanged(); }
    }

    public string Vehicul
    {
        get => vehicul;
        set { vehicul = value; OnPropertyChanged(); }
    }

    public DateTime DataStart
    {
        get => dataStart;
        set { dataStart = value; OnPropertyChanged(); OnPropertyChanged(nameof(Perioada)); }
    }

    public DateTime DataRetur
    {
        get => dataRetur;
        set { dataRetur = value; OnPropertyChanged(); OnPropertyChanged(nameof(Perioada)); OnPropertyChanged(nameof(NrZile)); }
    }

    public decimal CostTotal
    {
        get => costTotal;
        set { costTotal = value; OnPropertyChanged(); OnPropertyChanged(nameof(CostText)); }
    }

    public string Stare
    {
        get => stare;
        set { stare = value; OnPropertyChanged(); }
    }

    public double Rating
    {
        get => rating;
        set { rating = value; OnPropertyChanged(); OnPropertyChanged(nameof(RatingText)); }
    }

    // Proprietati derivate (Lab 9 pattern)
    public string Perioada  => $"{DataStart:dd.MM.yyyy} – {DataRetur:dd.MM.yyyy}";
    public int    NrZile    => Math.Max(1, (int)(DataRetur - DataStart).TotalDays);
    public string CostText  => $"{CostTotal:C2}";
    public string RatingText => $"{Rating:F1} / 10";

    public string Descriere =>
        $"#{Id} | {Client} | {Vehicul} | {Perioada} | {CostText} | {Stare}";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

/// <summary>
/// ViewModel pentru modulul WPF Rapoarte (Lab 9 – MVVM + INotifyPropertyChanged).
/// Expune ObservableCollection, proprietati cu binding si comenzi RelayCommand.
/// </summary>
public class RapoarteViewModel : INotifyPropertyChanged
{
    // ── Colectia principala (Lab 9 – ObservableCollection) ─────────────────
    public ObservableCollection<RezervareModel> Rezervari { get; } = new();

    // ── Comenzi (Lab 9 – RelayCommand / ICommand) ─────────────────────────
    public ICommand AdaugaCommand  { get; }
    public ICommand StergeCommand  { get; }
    public ICommand ExportCommand  { get; }
    public ICommand FiltreazaCommand { get; }

    // ── State pentru adaugare rezervare noua ──────────────────────────────
    private string nouClient  = string.Empty;
    private string nouVehicul = string.Empty;
    private int    nouAn      = DateTime.Now.Year;

    public string NouClient
    {
        get => nouClient;
        set
        {
            nouClient = value;
            OnPropertyChanged();
            ((RelayCommand)AdaugaCommand).Refresh();
        }
    }

    public string NouVehicul
    {
        get => nouVehicul;
        set { nouVehicul = value; OnPropertyChanged(); ((RelayCommand)AdaugaCommand).Refresh(); }
    }

    public int NouAn
    {
        get => nouAn;
        set { nouAn = value; OnPropertyChanged(); }
    }

    // ── Rezervarea selectata ───────────────────────────────────────────────
    private RezervareModel? rezervareSelectata;

    public RezervareModel? RezervareSelectata
    {
        get => rezervareSelectata;
        set
        {
            rezervareSelectata = value;
            OnPropertyChanged();
            ((RelayCommand)StergeCommand).Refresh();
        }
    }

    // ── Statistici (proprietati derivate cu binding) ───────────────────────
    private decimal totalIncasari;

    public decimal TotalIncasari
    {
        get => totalIncasari;
        set { totalIncasari = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalText)); }
    }

    public string TotalText => $"Total încasări: {TotalIncasari:C2}";

    private string filtruStare = "(Toate)";
    public string FiltruStare
    {
        get => filtruStare;
        set { filtruStare = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public RapoarteViewModel()
    {
        AdaugaCommand   = new RelayCommand(AdaugaRezervare,  _ => !string.IsNullOrWhiteSpace(NouClient) && !string.IsNullOrWhiteSpace(NouVehicul));
        StergeCommand   = new RelayCommand(StergeRezervare,  _ => RezervareSelectata != null);
        ExportCommand   = new RelayCommand(ExportRaport,     _ => Rezervari.Count > 0);
        FiltreazaCommand = new RelayCommand(FiltreazaRaport);

        // Date demonstrative
        IncarcaDateDemo();
    }

    private void IncarcaDateDemo()
    {
        var demo = new[]
        {
            new RezervareModel { Id=1, Client="Ion Popescu",   Vehicul="Dacia Logan",   DataStart=DateTime.Today.AddDays(-10), DataRetur=DateTime.Today.AddDays(-7), CostTotal=450, Stare="Finalizata", Rating=8.5 },
            new RezervareModel { Id=2, Client="Maria Ionescu", Vehicul="BMW X5",        DataStart=DateTime.Today.AddDays(-5),  DataRetur=DateTime.Today.AddDays(-2), CostTotal=1200, Stare="Finalizata", Rating=9.0 },
            new RezervareModel { Id=3, Client="Andrei Popa",   Vehicul="Skoda Octavia", DataStart=DateTime.Today,              DataRetur=DateTime.Today.AddDays(3),  CostTotal=540, Stare="Activa",     Rating=7.0 },
            new RezervareModel { Id=4, Client="Elena Dumitru", Vehicul="Ford Focus",    DataStart=DateTime.Today.AddDays(-2),  DataRetur=DateTime.Today.AddDays(1),  CostTotal=360, Stare="Activa",     Rating=8.0 },
        };
        foreach (var r in demo)
            Rezervari.Add(r);

        RecalculeazaTotal();
    }

    private void AdaugaRezervare(object? obj)
    {
        var rez = new RezervareModel
        {
            Id        = Rezervari.Count + 1,
            Client    = NouClient.Trim(),
            Vehicul   = NouVehicul.Trim(),
            DataStart = DateTime.Today,
            DataRetur = DateTime.Today.AddDays(NouAn > 0 ? NouAn : 1),
            CostTotal = 300,
            Stare     = "Activa",
            Rating    = 7.0
        };
        Rezervari.Add(rez);
        RezervareSelectata = rez;
        NouClient  = string.Empty;
        NouVehicul = string.Empty;
        NouAn      = 1;
        RecalculeazaTotal();
        ((RelayCommand)ExportCommand).Refresh();
    }

    private void StergeRezervare(object? obj)
    {
        if (RezervareSelectata == null) return;
        int idx = Rezervari.IndexOf(RezervareSelectata);
        Rezervari.Remove(RezervareSelectata);
        RezervareSelectata = Rezervari.Count > 0
            ? Rezervari[Math.Max(0, idx - 1)]
            : null;
        RecalculeazaTotal();
        ((RelayCommand)ExportCommand).Refresh();
    }

    private void ExportRaport(object? obj)
    {
        string dir  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RentCar_Rapoarte");
        Directory.CreateDirectory(dir);
        string cale = Path.Combine(dir, $"raport_wpf_{DateTime.Now:yyyyMMdd_HHmm}.txt");

        using var sw = new StreamWriter(cale, false, System.Text.Encoding.UTF8);
        sw.WriteLine("==============================");
        sw.WriteLine("   RAPORT REZERVARI – RENT CAR (WPF)");
        sw.WriteLine($"   Generat: {DateTime.Now:dd.MM.yyyy HH:mm}");
        sw.WriteLine("==============================");
        sw.WriteLine();
        foreach (var r in Rezervari)
            sw.WriteLine($"#{r.Id} | {r.Client,-20} | {r.Vehicul,-20} | {r.Perioada} | {r.CostText} | {r.Stare} | Rating: {r.RatingText}");
        sw.WriteLine();
        sw.WriteLine($"TOTAL: {TotalText}");

        System.Diagnostics.Process.Start("notepad.exe", cale);
    }

    private void FiltreazaRaport(object? obj)
    {
        // Filtrarea in WPF se face prin CollectionViewSource in View.
        // Aici se poate actualiza un flag care View-ul il observa prin binding.
        RecalculeazaTotal();
    }

    private void RecalculeazaTotal()
    {
        TotalIncasari = Rezervari.Sum(r => r.CostTotal);
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
