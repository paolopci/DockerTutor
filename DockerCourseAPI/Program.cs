using System.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:1234", "http://localhost:5163", "https://localhost:7163")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Aggiungi controller per una migliore struttura API
builder.Services.AddControllers();

var app = builder.Build();

// Configura la pipeline delle richieste HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowBlazorClient");

app.MapGet("/podcasts", async () =>
{
    try
    {
        // Prova prima la connessione Docker, poi locale
        var connectionString = Environment.GetEnvironmentVariable("DB_DOCKER") == "true" 
            ? "Server=database,1433;Database=PodDB;User Id=sa;Password=Micene@65;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;"
            : "Server=localhost,15001;Database=PodDB;User Id=sa;Password=Micene@65;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;";
        
        using var db = new SqlConnection(connectionString);
        await db.OpenAsync();
        
        var podcasts = await db.QueryAsync<Podcast>("SELECT Id, Title FROM Podcast");
        return Results.Ok(podcasts.Select(x => x.Title));
    }
    catch (SqlException ex)
    {
        Console.WriteLine($"Errore database: {ex.Message}");
        return Results.Problem($"Connessione al database fallita: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Errore generale: {ex.Message}");
        return Results.Problem($"Si è verificato un errore: {ex.Message}");
    }
});

// Aggiungi un semplice endpoint di controllo salute
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.Run();

record Podcast(string Id, string Title);