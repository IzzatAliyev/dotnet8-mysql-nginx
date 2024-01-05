using MySqlConnector;

namespace backend.config;
public static class DbExtensions
{
    static string password = File.ReadAllText("/run/secrets/db-password");
    static string connectionString = $"server=db;user=root;database=example;port=3306;password={password}";
    public static void AddStorage(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<MySqlConnection>((_provider) => new MySqlConnection(connectionString));
    }

    public static void DatabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);

        connection.Open();
        using var transation = connection.BeginTransaction();

        using MySqlCommand cmd1 = new MySqlCommand("DROP TABLE IF EXISTS blog", connection, transation);
        cmd1.ExecuteNonQuery();

        using MySqlCommand cmd2 = new MySqlCommand("CREATE TABLE IF NOT EXISTS blog (id int NOT NULL AUTO_INCREMENT, title varchar(255), PRIMARY KEY (id))", connection, transation);
        cmd2.ExecuteNonQuery();

        for (int i = 0; i < 5; i++)
        {
            using MySqlCommand insertCommand = new MySqlCommand($"INSERT INTO blog (title) VALUES ('Blog post #{i}');", connection, transation);
            insertCommand.ExecuteNonQuery();
        }
        transation.Commit();
        connection.Close();
    }
}
