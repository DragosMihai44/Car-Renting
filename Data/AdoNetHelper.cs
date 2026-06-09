using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RentCar.Data;

public class AdoNetHelper : IDisposable
{
    private readonly string connString;
    private SqlConnection? connection;
    private SqlDataAdapter? adapter;
    private SqlCommandBuilder? commandBuilder;

    public DataTable VehiculeTable { get; } = new DataTable("Vehicule");

    public AdoNetHelper()
    {
       
        connString = ConfigurationManager.ConnectionStrings["RentCar"]?.ConnectionString
                     ?? "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\rentcar.mdf;Integrated Security=True;";
    }

    public void Initialize()
    {
        connection = new SqlConnection(connString);
        adapter    = new SqlDataAdapter(
            "SELECT v.Id, v.Marca, v.Model, v.AnFabricatie, v.NrInmatriculare, " +
            "       c.Denumire AS Categorie, v.TarifZiLei, v.Kilometraj, v.Stare " +
            "FROM dbo.Vehicule v " +
            "INNER JOIN dbo.CategoriiVehicule c ON v.CategorieId = c.Id",
            connection);
        commandBuilder = new SqlCommandBuilder(adapter);
        LoadData();
    }

    public void LoadData()
    {
        if (adapter == null) return;
        VehiculeTable.Clear();
        adapter.Fill(VehiculeTable);
    }

    public void AdaugaVehicul(string marca, string model, int an, string nrInmatriculare,
                               int categorieId, decimal tarif, int km)
    {
        if (connection == null) return;
        using var cmd = new SqlCommand(
            "INSERT INTO dbo.Vehicule (Marca, Model, AnFabricatie, NrInmatriculare, CategorieId, TarifZiLei, Kilometraj, Stare, DataAdaugare) " +
            "VALUES (@marca, @model, @an, @nr, @catId, @tarif, @km, 'Disponibil', GETDATE())",
            connection);
        cmd.Parameters.AddWithValue("@marca",  marca);
        cmd.Parameters.AddWithValue("@model",  model);
        cmd.Parameters.AddWithValue("@an",     an);
        cmd.Parameters.AddWithValue("@nr",     nrInmatriculare);
        cmd.Parameters.AddWithValue("@catId",  categorieId);
        cmd.Parameters.AddWithValue("@tarif",  tarif);
        cmd.Parameters.AddWithValue("@km",     km);
        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        LoadData();
    }

    public void ModificaVehicul(int id, string marca, string model, int an, string nrInmatriculare,
                                 int categorieId, decimal tarif, int km, string stare)
    {
        if (connection == null) return;
        using var cmd = new SqlCommand(
            "UPDATE dbo.Vehicule SET Marca=@marca, Model=@model, AnFabricatie=@an, " +
            "NrInmatriculare=@nr, CategorieId=@catId, TarifZiLei=@tarif, Kilometraj=@km, Stare=@stare " +
            "WHERE Id=@id",
            connection);
        cmd.Parameters.AddWithValue("@marca",  marca);
        cmd.Parameters.AddWithValue("@model",  model);
        cmd.Parameters.AddWithValue("@an",     an);
        cmd.Parameters.AddWithValue("@nr",     nrInmatriculare);
        cmd.Parameters.AddWithValue("@catId",  categorieId);
        cmd.Parameters.AddWithValue("@tarif",  tarif);
        cmd.Parameters.AddWithValue("@km",     km);
        cmd.Parameters.AddWithValue("@stare",  stare);
        cmd.Parameters.AddWithValue("@id",     id);
        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        LoadData();
    }

    public void StergeVehicul(int id)
    {
        if (connection == null) return;
        using var cmd = new SqlCommand("DELETE FROM dbo.Vehicule WHERE Id=@id", connection);
        cmd.Parameters.AddWithValue("@id", id);
        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        LoadData();
    }

    public void ActualizeazaStare(int id, string stareNoua)
    {
        if (connection == null) return;
        using var cmd = new SqlCommand("UPDATE dbo.Vehicule SET Stare=@stare WHERE Id=@id", connection);
        cmd.Parameters.AddWithValue("@stare", stareNoua);
        cmd.Parameters.AddWithValue("@id",    id);
        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        LoadData();
    }

    public void Dispose()
    {
        connection?.Dispose();
        adapter?.Dispose();
    }
}
