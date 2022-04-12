using System.Text.Json.Serialization;
using App.DbConfigurations;
using App.Repository;
using App.Seeders;
using App.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var appPreset = new AppPresetManager(Directory.GetCurrentDirectory()).GetPreset();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot");

builder.Services.AddSingleton(appPreset);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.EnableAnnotations();
});

// string sqliteConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(appPreset.SqliteConnection)
);

// var corsSpaName = "_spa";
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(corsSpaName, corsPolicyBuilder =>
//     {
//         corsPolicyBuilder
//             .AllowAnyMethod()
//             .AllowAnyHeader()
//             .AllowCredentials()
//             .WithOrigins("https://localhost:7125", "http://localhost:3000")
//             ;
//     });
// });

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressInferBindingSourcesForParameters = true;
});

builder.Services.AddTransient<EntryRepository>();

// EntryInfo
builder.Services.AddTransient<EntryDateRepository>();
builder.Services.AddTransient<EntryEmailRepository>();
builder.Services.AddTransient<EntryNoteRepository>();
builder.Services.AddTransient<EntryPhoneRepository>();
builder.Services.AddTransient<EntryUrlRepository>();

builder.Services.AddTransient<EntryFileRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (app.Environment.IsDevelopment())
  {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        await new DatabaseSeeder(db).SeedAll();
    }

    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseDefaultFiles();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(appPreset.FilesDir),
    RequestPath = "/files"
});

app.Run();

public partial class Program
{
}