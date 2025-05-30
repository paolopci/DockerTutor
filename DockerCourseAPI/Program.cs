using System.Data.SqlClient;
using Dapper;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// 1) Definizione della policy CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy
            .WithOrigins("https://localhost:7083")  // Origine del client
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowBlazorClient");

app.MapGet("/podcasts", async () =>
{
    // Use Microsoft.Data.SqlClient instead of System.Data.SqlClient
    var db = new SqlConnection("Server=localhost,15001;Database=PodDB;User Id=sa;Password=Micene@65;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;");

    return (await db.QueryAsync<Podcast>("SELECT * from Podcast")).Select(x=>x.Title);

    //return podcasts;
    //return new List<string>
    //{
    //    "Unhandled Exception Podcast",
    //    "Developer Weekly Podcast",
    //    "The Stack Overflow Podcast",
    //    "The Hanselminutes Podcast",
    //    "The .NET Rocks Podcast",
    //    "The Azure Podcast",
    //    "The AWS Podcast",
    //    "The Rabbit Hole Podcast",
    //    "The .NET Core Podcast",
    //};
});

app.Run();

record Podcast(string Id, string Title);
