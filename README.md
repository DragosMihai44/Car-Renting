# 🚗 Rent Car – Aplicație Desktop .NET

## Materia: MSOA – Universitatea Politehnică Timișoara

Aplicație completă de gestiune pentru o firmă de închiriere auto, implementând toate conceptele din laboratoarele 1–9 MSOA.

---

## Structura proiectului

```
RentCar/
├── Models/              # Clasele de date (EF Code First)
│   ├── Enums.cs         # Enumerări: StareVehicul, TipClient, StareRezervare, RolUtilizator
│   ├── Utilizator.cs    # Model utilizator cu hashing parola
│   ├── CategorieVehicul.cs
│   ├── Vehicul.cs       # Proprietati + atribute [Description, Category]
│   ├── Client.cs        # Proprietati pentru PropertyGrid (Lab 3)
│   └── Rezervare.cs     # Rezervare + Contract + Returnare
├── Data/
│   ├── RentCarDbContext.cs   # Entity Framework Core context (Lab 6)
│   ├── AdoNetHelper.cs       # ADO.NET DataAdapter + SqlCommandBuilder (Lab 5)
│   └── create_database.sql  # Script SQL Server pentru modulul Flotă (Lab 5)
├── Comparers/
│   └── VehiculComparers.cs  # IComparer<Vehicul>, IComparer<Client> (Lab 2)
├── Reports/
│   └── ReportHelper.cs      # StreamWriter – salvare rapoarte fișier text (Lab 3)
├── Forms/
│   ├── FrmLogin.cs          # Autentificare + ProgressBar + Timer (Lab 6)
│   ├── FrmPrincipal.cs      # Fereastra principala cu TabControl
│   ├── FrmVehicul.cs        # Dialog vehicul + ErrorProvider (Lab 4 + Lab 5)
│   ├── FrmClient.cs         # Dialog client + ErrorProvider (Lab 4)
│   ├── FrmRezervare.cs      # Dialog rezervare + calcul cost (Lab 6)
│   ├── FrmReturnare.cs      # Dialog returnare + calcul penalizare
│   ├── FrmFlota.cs          # DataGridView + ADO.NET + filtrare/sortare (Lab 2, 5)
│   ├── FrmClienti.cs        # TreeView + PropertyGrid + fișier text (Lab 3, 4)
│   ├── FrmRezervari.cs      # ListBox clienți + rezervări + EF Core (Lab 6)
│   ├── FrmService.cs        # FlowLayoutPanel + butoane dinamice (Lab 4)
│   └── FrmRapoarte.cs       # Statistici + export fișier (Lab 3)
├── WPF/
│   ├── Commands/RelayCommand.cs      # ICommand pattern (Lab 9)
│   ├── ViewModels/RapoarteViewModel.cs # MVVM + INotifyPropertyChanged + ObservableCollection (Lab 9)
│   ├── Views/MainWindow.xaml          # WPF XAML cu data binding (Lab 8 + Lab 9)
│   ├── Views/MainWindow.xaml.cs       # Code-behind minimal
│   ├── App.xaml
│   └── RentCarWPF.csproj
├── Program.cs           # Entry point (Lab 1)
├── App.config           # Connection string SQL Server (Lab 5)
└── RentCar.csproj
```

---

## Concepte implementate per laborator

| Lab | Concept | Implementare în Rent Car |
|-----|---------|--------------------------|
| Lab 1 | WinForms, Application.Run, Event-driven | `Program.cs`, `FrmPrincipal.cs` |
| Lab 2 | Filtrare, sortare, IComparer | `FrmFlota.cs` (filtru categorie/stare), `VehiculComparers.cs` |
| Lab 3 | TreeView, PropertyGrid, StreamWriter, căutare | `FrmClienti.cs`, `ReportHelper.cs` |
| Lab 4 | Controale dinamice, FlowLayoutPanel, dialog modal, ErrorProvider | `FrmService.cs`, `FrmVehicul.cs`, `FrmClient.cs` |
| Lab 5 | ADO.NET, DataAdapter, SqlCommandBuilder, DataGridView | `FrmFlota.cs`, `AdoNetHelper.cs` |
| Lab 6 | Entity Framework Code First, DbContext, cascade delete, FormLogin, Timer, ProgressBar | `RentCarDbContext.cs`, `FrmRezervari.cs`, `FrmLogin.cs` |
| Lab 8 | WPF, XAML, Grid, StackPanel, DockPanel, TextBlock, TextBox | `MainWindow.xaml` |
| Lab 9 | MVVM, INotifyPropertyChanged, ObservableCollection, RelayCommand, data binding, Slider | `RapoarteViewModel.cs`, `MainWindow.xaml` |

---

## Setup și rulare

### Cerințe
- Visual Studio 2022 (sau mai nou)
- .NET 10.0 SDK
- SQL Server LocalDB (vine cu Visual Studio)

### Pași

#### 1. Proiect WinForms (Rent Car principal)

1. Deschide `RentCar.csproj` în Visual Studio.
2. Instalează pachetele NuGet (se fac automat la restore):
   - `Microsoft.Data.SqlClient`
   - `Microsoft.EntityFrameworkCore`
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `Microsoft.EntityFrameworkCore.Tools`
   - `System.Configuration.ConfigurationManager`
3. **Pentru modulul Flotă (ADO.NET – Lab 5):**
   - În Visual Studio: `Project → Add New Item → Service-based Database` → numește-l `rentcar.mdf`
   - Setează proprietatea `Copy to Output Directory: Copy if newer`
   - Rulează scriptul din `Data/create_database.sql` via `Server Explorer → Tables → New Query`
4. **Pentru modulele cu EF Core (Lab 6):**
   - Baza de date SQLite `rentcar.db` se creează automat la prima rulare
5. Apasă `F5` pentru a rula.
6. Login cu: `admin/admin123`, `agent/agent123` sau `mecanic/mecanic123`

#### 2. Proiect WPF (Rapoarte)

1. Deschide `WPF/RentCarWPF.csproj` în Visual Studio.
2. Apasă `F5` pentru a rula interfața WPF de rapoarte.

---

## Credențiale implicite

| Utilizator | Parolă      | Rol           | Acces |
|------------|-------------|---------------|-------|
| admin      | admin123    | Administrator | Toate modulele |
| agent      | agent123    | Agent         | Clienți, Rezervări, Flotă (read) |
| mecanic    | mecanic123  | Mecanic       | Service |
