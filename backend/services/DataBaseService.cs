namespace backend.services;

using MySqlConnector;

public class DataBaseService
{
    static string password = File.ReadAllText("/run/secrets/db-password");
    static string connectionString = $"server=db;user=root;database=example;port=3306;password={password}";

    public MySqlConnection Connect()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        return connection;
    }
}