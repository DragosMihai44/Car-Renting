-- =============================================================
-- Rent Car – Script creare baza de date SQL Server (Lab 5)
-- Ruleaza in SQL Server Management Studio sau din Visual Studio
-- Server Explorer -> New Query
-- =============================================================

CREATE TABLE [dbo].[CategoriiVehicule] (
    [Id]         INT IDENTITY(1,1) PRIMARY KEY,
    [Denumire]   NVARCHAR(100) NOT NULL,
    [TarifMinim] DECIMAL(10,2) NOT NULL DEFAULT (0),
    [TarifMaxim] DECIMAL(10,2) NOT NULL DEFAULT (0)
);

CREATE TABLE [dbo].[Vehicule] (
    [Id]              INT IDENTITY(1,1) PRIMARY KEY,
    [Marca]           NVARCHAR(100) NOT NULL,
    [Model]           NVARCHAR(100) NOT NULL,
    [AnFabricatie]    INT           NOT NULL,
    [NrInmatriculare] NVARCHAR(20)  NOT NULL UNIQUE,
    [CategorieId]     INT           NOT NULL
        CONSTRAINT FK_Vehicule_Categorie REFERENCES [dbo].[CategoriiVehicule]([Id]),
    [TarifZiLei]      DECIMAL(10,2) NOT NULL
        CONSTRAINT DF_Vehicule_Tarif DEFAULT (0),
    [Kilometraj]      INT           NOT NULL
        CONSTRAINT DF_Vehicule_Km DEFAULT (0),
    [Stare]           NVARCHAR(30)  NOT NULL
        CONSTRAINT DF_Vehicule_Stare DEFAULT ('Disponibil'),
    [DataAdaugare]    DATE          NOT NULL
        CONSTRAINT DF_Vehicule_Data DEFAULT (GETDATE()),
    [DataReviziei]    DATE          NOT NULL
        CONSTRAINT DF_Vehicule_Revizie DEFAULT (DATEADD(MONTH,6,GETDATE())),
    [DataITP]         DATE          NOT NULL
        CONSTRAINT DF_Vehicule_ITP DEFAULT (DATEADD(YEAR,1,GETDATE()))
);


INSERT INTO [dbo].[CategoriiVehicule] (Denumire, TarifMinim, TarifMaxim) VALUES
    ('Mica',       80,  120),
    ('Compacta',   120, 180),
    ('SUV',        200, 350),
    ('Premium',    350, 600),
    ('Utilitara',  150, 250);

INSERT INTO [dbo].[Vehicule] (Marca, Model, AnFabricatie, NrInmatriculare, CategorieId, TarifZiLei, Kilometraj, Stare) VALUES
    ('Dacia',   'Logan',        2021, 'TM-01-ABC', 1, 95,  45000, 'Disponibil'),
    ('Skoda',   'Octavia',      2022, 'TM-02-DEF', 2, 150, 30000, 'Disponibil'),
    ('BMW',     'X5',           2023, 'TM-03-GHI', 3, 280, 15000, 'Disponibil'),
    ('Mercedes','Clasa C',      2023, 'TM-04-JKL', 4, 450, 12000, 'Inchiriat'),
    ('Renault', 'Kangoo',       2020, 'TM-05-MNO', 5, 180, 80000, 'Disponibil'),
    ('Volkswagen','Golf',       2022, 'TM-06-PQR', 2, 140, 25000, 'InService'),
    ('Toyota',  'Corolla',      2021, 'TM-07-STU', 2, 130, 35000, 'Disponibil'),
    ('Audi',    'A6',           2022, 'TM-08-VWX', 4, 400, 18000, 'Disponibil');
