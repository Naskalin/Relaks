using System.Text.Json.Serialization;
using App.DbConfigurations;
using App.DbEvents.Fts;
using App.Endpoints.StructureItems;
using App.Repository;
using App.Seeders;
using App.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var appPreset = new AppPresetManager(Directory.GetCurrentDirectory()).GetPreset();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot");

builder.Services.AddSingleton(appPreset);

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
    options => options.UseSqlite(appPreset.SqliteConnection)
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    
    EntryEvents.CheckAndRefresh(db);
    EntryInfoEvents.CheckAndRefresh(db);
    
    if (app.Environment.IsDevelopment()) new DatabaseSeeder(db).SeedAll();
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