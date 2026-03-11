
using NotesBoard.Data;
using NotesBoard.Data.Repository;
using NotesBoard.Data.Repository.Contract;
using NotesBoard.Services;
using NotesBoard.Services.Contract;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting NotesBoard application");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/notesboard-.txt", 
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    );

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddSingleton<BoardDbContext>();
    builder.Services.AddScoped<INoteBoardRepository, NoteBoardRepository>();
    builder.Services.AddScoped<INoteService, NoteService>();

    WebApplication app = builder.Build();

    ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("NotesBoard application configured successfully");

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    logger.LogInformation("NotesBoard application ready to accept requests");

    await app.RunAsync();

    logger.LogInformation("NotesBoard application stopped gracefully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.Information("Shutting down NotesBoard application");
    await Log.CloseAndFlushAsync();
}
