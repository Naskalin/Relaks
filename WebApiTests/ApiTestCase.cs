using App;
using Microsoft.EntityFrameworkCore;

namespace WebApiTests;

public class ApiTestCase
{
    protected readonly ApplicationContext Db;

    protected ApiTestCase()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite("Data Source=C:\\app\\RiderProjects\\Ras\\WebApiTests\\app_test.db")
            // .UseInMemoryDatabase(databaseName: "AppTestDb")
            .Options;
        Db = new ApplicationContext(dbContextOptions);
        Db.Database.EnsureCreated();
        
        Db.Entries.RemoveRange(Db.Entries);
        Db.SaveChanges();
    }
}