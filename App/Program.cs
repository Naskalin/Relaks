using System.Text.Json.Serialization;
using App.Data;
// using App.ResourceDefinitions;
using App.Seeders;
using JsonApiDotNetCore.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    // .AddNewtonsoftJson(setupAction =>
    // {
    //     setupAction.SerializerSettings.Converters.Add(new StringEnumConverter());
    // })
    // .AddNewtonsoftJson()
    // .AddOData(
    //     options => options
    //         .EnableQueryFeatures()
    //         .Select()
    //         .Filter()
    //         .OrderBy()
    //         .Count()
    // )
    .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
    
    ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqliteConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(sqliteConnection)
);

builder.Services.AddJsonApi<AppDbContext>(options =>
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
// builder.Services.AddResourceDefinition<EntryDefinition>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    if (app.Environment.IsDevelopment())
    {
        await new DatabaseSeeder(db).seed();
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