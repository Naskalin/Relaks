using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class DatabaseSeeder
{
    protected readonly AppDbContext Db;

    public DatabaseSeeder(AppDbContext db)
    {
        Db = db;
    }

    public async Task SeedAll()
    {
        // var person = new Person() {Name = "Tom"};
        // Db.Entries.Add(person);
        //
        // var tag = new EntryTag() {Title = "Test tag"};
        // Db.EntryTags.Add(tag);
        //
        // person.Tags.Add(tag);

        // var book = new Book() {Title = "first book"};
        // Db.Books.Add(book);
        //
        // var author = new Author() {Name = "super author"};
        // Db.Authors.Add(author);
        //
        // book.Authors.Add(author);

        // await Db.SaveChangesAsync();


        // Db.EntryTags.Add(tag);
        // person.Tags.Add(tag);

        // var tags = new[] {"Программист", "Финансы", "Аниматор", "Видеооператор"};
        await new EntrySeeder(Db).Seed();
        await new InfoDateSeeder(Db).Seed();
    }
}