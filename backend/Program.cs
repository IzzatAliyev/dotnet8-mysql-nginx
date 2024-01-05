using backend.config;
using backend.services;
using MySqlConnector;

namespace backend;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddStorage();
        builder.Services.AddSingleton<DataBaseService>();
        builder.Services.AddSingleton<IHomeService, HomeService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();
        app.MapControllers();
        app.DatabaseEnsureCreated();

        app.Run();
    }
}