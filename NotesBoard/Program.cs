
using NotesBoard.Data;
using NotesBoard.Data.Repository;
using NotesBoard.Data.Repository.Contract;
using NotesBoard.Services;
using NotesBoard.Services.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<BoardDbContext>();
builder.Services.AddScoped<INoteBoardRepository, NoteBoardRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
