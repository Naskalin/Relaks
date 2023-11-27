using System.Text.Json;
using Relaks.Managers;
using Relaks.Models;
using Relaks.Models.FinancialModels;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    private class JsonCountry
    {
        public required string Title { get; set; }
        public required string Symbol { get; set; }
    }
    private void SeedFinancials()
    {
        var entry = Db.BaseEntries.First(x => x.Id.Equals(Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4")));
        if (!Db.FinancialCurrencies.Any()) CreateCurrencies();
        if (!Db.FinancialAccounts.Any()) CreateFinancialAccounts(entry);
        if (!Db.FinancialTransactionCategories.Any()) CreateFinancialTransactionCategories();
    }

    private void CreateFinancialTransactionCategories()
    {
        var parent1 = new FinancialTransactionCategory
        {
            Title = "Дом"
        };
        TreeManager.UpdateTreePath(parent1);
    }

    private void CreateFinancialAccounts(BaseEntry entry)
    {
        var rub = Db.FinancialCurrencies.First(x => x.Id.Equals("RUB"));
        var usd = Db.FinancialCurrencies.First(x => x.Id.Equals("USD"));

        var accounts = new List<FinancialAccount>();
        foreach (var currency in new [] {rub, usd})
        {
            var item = new FinancialAccount
            {
                EntryId = entry.Id,
                Description = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), null}),
                Title = Faker.Random.Words(Faker.Random.Int(5, 20)),
                FinancialCurrencyId = currency.Id,
                StartAt = default,
            };
            if (Faker.Random.Int(1, 4) >= 2)
            {
                item.EndAt = Faker.Date.Soon(365, item.StartAt);
            }
            accounts.Add(item);
        }
        
        
        Db.FinancialAccounts.AddRange(accounts);
        Db.SaveChanges();
    }
    
    private void CreateCurrencies()
    {
        var projectDir = AppDomain.CurrentDomain.BaseDirectory;
        using StreamReader r
            = new StreamReader(Path.Combine(projectDir, "src", "Database", "Seeders", "currencies.json"));
        string json = r.ReadToEnd();
        var jsonCurrencies = JsonSerializer.Deserialize<Dictionary<string, JsonCountry>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (jsonCurrencies == null) return;
        
        var currencies = new List<FinancialCurrency>();
        foreach (var item in jsonCurrencies)
        {
            currencies.Add(new FinancialCurrency()
            {
                Id = item.Key,
                Title = item.Value.Title,
                Symbol = item.Value.Symbol,
            });
        }
        
        Db.FinancialCurrencies.AddRange(currencies);
        Db.SaveChanges();
    }
}