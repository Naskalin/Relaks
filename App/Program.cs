global using FastEndpoints;
global using FluentValidation;

using System.Text.Json.Serialization;
using App.DbConfigurations;
using App.DbEvents.Fts;
using App.Endpoints.StructureItems;
using App.Repository;
using App.Utils.App;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseElectron(args);

var projectDir = Directory.GetCurrentDirectory();
var appDataDir = AppDataDirManager.GetDirPath(projectDir);
var connectionString = "";
if (appDataDir != null)
{
    builder.Services.AddSingleton(appDataDir);
    var appPreset = AppPresetManager.GetPreset(projectDir);

    if (appPreset != null)
    {
        builder.Services.AddSingleton(appPreset);
        connectionString = appPreset.SqliteConnection;
    }
}

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(connectionString)
);

// builder.Services.Configure<ApiBehaviorOptions>(o => { o.SuppressInferBindingSourcesForParameters = true; });
builder.Services.AddTransient<EntryRepository>();
builder.Services.AddTransient<EntryInfoRepository>();
builder.Services.AddTransient<InfoTemplateRepository>();
builder.Services.AddTransient<EntryFileRepository>();
builder.Services.AddTransient<StructureRepository>();
builder.Services.AddTransient<StructureItemRepository>();
builder.Services.AddTransient<StructureConnectionRepository>();
builder.Services.AddTransient<FileCategoryRepository>();
builder.Services.AddTransient<StructureItemDbValidate>();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    // c.SerializerOptions = options =>
    // {
    //     options.Converters.Add(new JsonStringEnumConverter());
    //     options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //     // options.WriteIndented = true;
    // };
    // c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
    c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Electron Window
if (HybridSupport.IsElectronActive)
{
    Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
    {
        Width = 1200,
        Height = 864,
        Center = true,
        Closable = true,
        Minimizable = true,
        Maximizable = true,
        AutoHideMenuBar = true,
        BackgroundColor = "#E0E7F7"
    }));
}

if (!String.IsNullOrEmpty(connectionString))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    
    EntryEvents.CheckAndRefresh(db);
    EntryInfoEvents.CheckAndRefresh(db);
    
    // if (app.Environment.IsDevelopment()) new DatabaseSeeder(db).SeedAll();
}

app.UseFileServer();
app.Run();