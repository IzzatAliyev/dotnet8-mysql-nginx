namespace backend.services;

using backend.models;
using MySqlConnector;

public class HomeService : IHomeService
{
    private DataBaseService dataBaseService;
    public HomeService(DataBaseService dataBaseService)
    {
        this.dataBaseService = dataBaseService;
    }

    public void CreateBlog(BlogJsonModel blog)
    {
        MySqlConnection connection = this.dataBaseService.Connect();

        connection.Open();
        using var transation = connection.BeginTransaction();

        using MySqlCommand insertCommand = new MySqlCommand($"INSERT INTO blog (title) VALUES (@title);", connection, transation);
        insertCommand.Parameters.AddWithValue("@title", blog.Title);
        insertCommand.ExecuteNonQuery();

        transation.Commit();
        connection.Close();
    }

    public BlogJsonModel[] GetBlogs()
    {
        MySqlConnection connection = this.dataBaseService.Connect();

        var blogs = new List<BlogJsonModel>();

        try
        {
            Console.WriteLine("Connecting to MySQL...");
            connection.Open();

            string sql = "SELECT id, title FROM blog";
            using var cmd = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader.GetInt32("id").ToString();
                string title = reader.GetString("title");
                var blog = new BlogJsonModel{
                    Id = id,
                    Title = title,
                };
                blogs.Add(blog);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return [.. blogs];
        }
        connection.Close();

        foreach(var blog in blogs){
            System.Console.WriteLine($"id = {blog.Id}, title = {blog.Title}");
        }

        return [.. blogs];
    }
}
