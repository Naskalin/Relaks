using System.Text.Json.Serialization;
using App.DbConfigurations;
using App.Repository;
using App.Seeders;
using DotNext.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new OptionalConverterFactory());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    
});

string sqliteConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(sqliteConnection)
);

var corsSpaName = "_spa";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsSpaName, corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("https://localhost:7125", "http://localhost:3000")
            ;
    });
});

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressInferBindingSourcesForParameters = true;
});
builder.Services.AddTransient<EntryRepository>();

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

app.UseCors(corsSpaName);
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program
{
}