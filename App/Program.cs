using System.Text.Json.Serialization;
using App.DbConfigurations;
using App.DbEvents.Fts;
using App.Endpoints.StructureItems;
using App.Repository;
using App.Utils.App;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
    c.EnableAnnotations();
});

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(connectionString)
);

builder.Services.Configure<ApiBehaviorOptions>(o => { o.SuppressInferBindingSourcesForParameters = true; });
builder.Services.AddTransient<EntryRepository>();
builder.Services.AddTransient<EntryInfoRepository>();
builder.Services.AddTransient<InfoTemplateRepository>();
builder.Services.AddTransient<EntryFileRepository>();
builder.Services.AddTransient<StructureRepository>();
builder.Services.AddTransient<StructureItemRepository>();
builder.Services.AddTransient<StructureConnectionRepository>();
builder.Services.AddTransient<StructureItemDbValidate>();

var app = builder.Build();

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
        // AutoHideMenuBar = true,
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
}

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();
app.MapControllers();
app.Run();
public partial class Program
{
}