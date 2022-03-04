using App;
using App.Models.Entry;
using App.Seeders;
using JsonApiDotNetCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(setupAction =>
    {
        setupAction.SerializerSettings.Converters.Add(new StringEnumConverter());
    })
    // .AddNewtonsoftJson()
    // .AddOData(
    //     options => options
    //         .EnableQueryFeatures()
    //         .Select()
    //         .Filter()
    //         .OrderBy()
    //         .Count()
    // )
    // .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
    
    ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqliteConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlite(sqliteConnection)
);

builder.Services.AddJsonApi<ApplicationContext>(options =>
{
    options.Namespace = "api";
    options.IncludeTotalResourceCount = true;
});

var corsSpaName = "_spa";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsSpaName, corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:7125", "http://localhost:3000")
            .AllowCredentials()
            ;
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
    if (app.Environment.IsDevelopment())
    {
        
        new EntrySeeder(db).Seed();
        await db.SaveChangesAsync();   
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}
// app.UseForwardedHeaders();
app.UseCors(corsSpaName);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseJsonApi();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program
{
}