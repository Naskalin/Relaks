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
        if (!Db.FinancialAccountCategories.Any()) CreateFinancialAccountCategories(entry);
        if (!Db.FinancialCurrencies.Any()) CreateCurrencies();
        if (!Db.FinancialAccounts.Any()) CreateFinancialAccounts();
        if (!Db.FinancialTransactionCategories.Any()) CreateFinancialTransactionCategories();
        if (!Db.EntryFinancialTransactions.Any()) CreateFinancialTransactions();
    }

    private void CreateFinancialAccountCategories(BaseEntry entry)
    {
        Db.FinancialAccountCategories.Add(new() {Title = "Наличные", EntryId = entry.Id});
        Db.FinancialAccountCategories.Add(new() {Title = "Кредитные карты", EntryId = entry.Id});
        
        // var entry2Id = Db.BaseEntries.Where(x => !x.Id.Equals(entry.Id)).Select(x => x.Id).First();
        // Db.FinancialAccountCategories.Add(new() {Title = "Наличные", EntryId = entry2Id});
        // Db.FinancialAccountCategories.Add(new() {Title = "Кредитные карты", EntryId = entry2Id});
        Db.SaveChanges();
    }

    private EntryFinancialTransaction? PreviousTransaction { get; set; }
    private void CreateFinancialTransactions()
    {
        var account = Db.FinancialAccounts.First(x => x.FinancialCurrencyId.Equals("RUB"));
        var entryId = Guid.Parse("01B137DA-A3CF-4C08-AC3E-752B3F156ED4");
        var categories = Db.FinancialTransactionCategories.ToDictionary(x => x.Title, x => x.Id);

        var transactions = new List<EntryFinancialTransaction>();
        for (int i = 0; i < 500; i++)
        {
          transactions.Add(new()
          {
            AccountId = account.Id,
            EntryId = entryId,
            Description = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), null}),
            IsPlus = Faker.Random.Bool(),
            CreatedAt = Faker.Date.Past(),
            Items = [
              new() {CategoryId = categories["Чипсы"], Quantity = Faker.Random.Int(1, 3), Amount = Math.Round(Faker.Random.Decimal(300, 600), 2)},
              new() {CategoryId = categories["Сигареты"], Quantity = Faker.Random.Int(1, 3), Amount = Math.Round(Faker.Random.Decimal(200, 800), 2)},
              new() {CategoryId = categories["Мясо"], Quantity = 1, Amount = Math.Round(Faker.Random.Decimal(800, 1500), 2)}
            ]
          });
          transactions.Add(new()
          {
            CreatedAt = Faker.Date.Past(),
            AccountId = account.Id,
            EntryId = entryId,
            Description = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), null}),
            IsPlus = Faker.Random.Bool(),
            Items = [
              new() {CategoryId = categories["Молоко"], Quantity = 2, Amount = Math.Round(Faker.Random.Decimal(250, 400), 2)},
            ]
          });
        }

        transactions = transactions.OrderBy(x => x.CreatedAt).ToList();
        
        foreach (var transaction in transactions)
        {
          transaction.UpdateTotal();
          transaction.UpdateBalance(PreviousTransaction?.Balance ?? account.Balance);
          PreviousTransaction = transaction;
        }
          
        account.Balance = transactions.Last().Balance;
        Db.EntryFinancialTransactions.AddRange(transactions);
        
        Db.SaveChanges();
    }

    private void CreateFinancialTransactionCategories()
    {
        var parent1 = new FinancialTransactionCategory {Title = "Дом"};
        var parent2 = new FinancialTransactionCategory {Title = "Покупки"};
            var child21 = new FinancialTransactionCategory {Title = "Питание", Parent = parent2};
                var child211 = new FinancialTransactionCategory {Title = "Продукты", Parent = child21};
                    var child2111 = new FinancialTransactionCategory {Title = "Молоко", Parent = child211};
                    var child2112 = new FinancialTransactionCategory {Title = "Мясо", Parent = child211};
                var child212 = new FinancialTransactionCategory {Title = "Вредности", Parent = child21};
                    var child2121 = new FinancialTransactionCategory {Title = "Сигареты", Parent = child212};
                    var child2122 = new FinancialTransactionCategory {Title = "Чипсы", Parent = child212};
        var categories = new List<FinancialTransactionCategory>()
        {
            parent1,
            new() {Title = "Ремонт", Parent = parent1},
            new() {Title = "Обслуживание", Parent = parent1},
            parent2,
            child21,
            child211,
            child212,
            child2111,
            child2112,
            child2121,
            child2122
        };
        categories.ForEach(x => x.TreePath = "");
        Db.FinancialTransactionCategories.AddRange(categories);
        Db.SaveChanges();
        categories.ForEach(x => TreeManager.UpdateTreePath(x, x.Parent));
        Db.SaveChanges();
    }

    private void CreateFinancialAccounts()
    {
        var categoryIds = Db.FinancialAccountCategories.Select(x => x.Id).ToList();
        var accounts = new List<FinancialAccount>();
        var i = 0;
        foreach (var categoryId in categoryIds)
        {
          for (int j = 0; j < 3; j++)
          {
            var currencyId = "RUB";
            if (j != 0)
            {
              currencyId = Faker.Random.ArrayElement(new[] {"RUB", "EUR", "USD"}); 
            }
            var account = new FinancialAccount
            {
              Description = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), null}),
              Title = Faker.Random.ArrayElement(new [] {"Сбережения", "На путешествия", "На бучение"}) + " #" + i,
              FinancialCurrencyId = currencyId,
              StartAt = DateTime.Now,
              Balance = 10000,
              CategoryId = categoryId,
            };
            
            if (Faker.Random.Int(1, 4) >= 2)
            {
              account.EndAt = Faker.Date.Soon(365, account.StartAt);
            }
            
            accounts.Add(account);
            i++;
          }
        }
        
        Db.FinancialAccounts.AddRange(accounts);
        Db.SaveChanges();
    }
    
    private void CreateCurrencies()
    {
        var jsonCurrencies = JsonSerializer.Deserialize<Dictionary<string, JsonCountry>>(CurrenciesJson, new JsonSerializerOptions(JsonSerializerDefaults.Web));
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

    private static readonly string CurrenciesJson = @"{
  ""AUD"": {
    ""title"": ""Доллар (австралийский доллар)"",
    ""symbol"": ""$""
  },
  ""EUR"": {
    ""title"": ""Евро"",
    ""symbol"": ""€""
  },
  ""AZN"": {
    ""title"": ""Манат (азербайджанский манат)"",
    ""symbol"": ""₼""
  },
  ""ALL"": {
    ""title"": ""Лек"",
    ""symbol"": ""L""
  },
  ""DZD"": {
    ""title"": ""Динар (алжирский динар)"",
    ""symbol"": "".د.ج • DA""
  },
  ""USD"": {
    ""title"": ""Доллар (доллар США)"",
    ""symbol"": ""$""
  },
  ""XCD"": {
    ""title"": ""Доллар (восточно-карибский доллар)"",
    ""symbol"": ""$""
  },
  ""AOA"": {
    ""title"": ""Кванза"",
    ""symbol"": ""Kz""
  },
  ""ARS"": {
    ""title"": ""Песо (аргентинское песо)"",
    ""symbol"": ""$""
  },
  ""AMD"": {
    ""title"": ""Драм (армянский драм)"",
    ""symbol"": ""֏""
  },
  ""AWG"": {
    ""title"": ""Флорин (арубанский флорин)"",
    ""symbol"": ""ƒ""
  },
  ""AFN"": {
    ""title"": ""Афгани"",
    ""symbol"": ""؋ • Af""
  },
  ""BSD"": {
    ""title"": ""Доллар (багамский доллар)"",
    ""symbol"": ""$""
  },
  ""BDT"": {
    ""title"": ""Така"",
    ""symbol"": ""৳""
  },
  ""BBD"": {
    ""title"": ""Доллар (барбадосский доллар)"",
    ""symbol"": ""$""
  },
  ""BHD"": {
    ""title"": ""Динар (бахрейнский динар)"",
    ""symbol"": "".د.ب • BD""
  },
  ""BZD"": {
    ""title"": ""Доллар (белизский доллар)"",
    ""symbol"": ""$""
  },
  ""BYN"": {
    ""title"": ""Рубль (белорусский рубль)"",
    ""symbol"": ""Br""
  },
  ""XOF"": {
    ""title"": ""Франк (франк КФА BCEAO)"",
    ""symbol"": ""₣""
  },
  ""BMD"": {
    ""title"": ""Доллар (бермудский доллар)"",
    ""symbol"": ""$""
  },
  ""BGN"": {
    ""title"": ""Лев (болгарский лев)"",
    ""symbol"": ""лв""
  },
  ""BOB"": {
    ""title"": ""Боливиано"",
    ""symbol"": ""$""
  },
  ""BAM"": {
    ""title"": ""Марка (конвертируемая марка)"",
    ""symbol"": ""KM""
  },
  ""BWP"": {
    ""title"": ""Пула"",
    ""symbol"": ""P""
  },
  ""BRL"": {
    ""title"": ""Реал (бразильский реал)"",
    ""symbol"": ""$""
  },
  ""BND"": {
    ""title"": ""Доллар (брунейский доллар)"",
    ""symbol"": ""$""
  },
  ""BIF"": {
    ""title"": ""Франк (бурундийский франк)"",
    ""symbol"": ""₣""
  },
  ""BTN"": {
    ""title"": ""Нгултрум[13]"",
    ""symbol"": ""Nu""
  },
  ""VUV"": {
    ""title"": ""Вату"",
    ""symbol"": ""Vt""
  },
  ""GBP"": {
    ""title"": ""Фунт (фунт стерлингов)"",
    ""symbol"": ""£""
  },
  ""HUF"": {
    ""title"": ""Форинт"",
    ""symbol"": ""Ft""
  },
  ""VES"": {
    ""title"": ""Суверенный боливар"",
    ""symbol"": ""Bs. S.""
  },
  ""VND"": {
    ""title"": ""Донг"",
    ""symbol"": ""₫""
  },
  ""XAF"": {
    ""title"": ""Франк (франк КФА BEAC)"",
    ""symbol"": ""₣""
  },
  ""HTG"": {
    ""title"": ""Гурд[14]"",
    ""symbol"": ""G""
  },
  ""GYD"": {
    ""title"": ""Доллар (гайанский доллар)"",
    ""symbol"": ""$""
  },
  ""GMD"": {
    ""title"": ""Даласи"",
    ""symbol"": ""D""
  },
  ""GHS"": {
    ""title"": ""Седи (ганский седи)"",
    ""symbol"": ""₵""
  },
  ""GTQ"": {
    ""title"": ""Кетсаль"",
    ""symbol"": ""Q""
  },
  ""GNF"": {
    ""title"": ""Франк (гвинейский франк)"",
    ""symbol"": ""₣""
  },
  ""GIP"": {
    ""title"": ""Фунт (гибралтарский фунт)"",
    ""symbol"": ""£""
  },
  ""HNL"": {
    ""title"": ""Лемпира"",
    ""symbol"": ""L""
  },
  ""HKD"": {
    ""title"": ""Доллар (гонконгский доллар)"",
    ""symbol"": ""$""
  },
  ""DKK"": {
    ""title"": ""Крона (датская крона)"",
    ""symbol"": ""kr""
  },
  ""GEL"": {
    ""title"": ""Лари"",
    ""symbol"": ""₾""
  },
  ""DJF"": {
    ""title"": ""Франк (франк Джибути)"",
    ""symbol"": ""₣""
  },
  ""DOP"": {
    ""title"": ""Песо (доминиканское песо)"",
    ""symbol"": ""$""
  },
  ""EGP"": {
    ""title"": ""Фунт (египетский фунт)"",
    ""symbol"": "".ج.م • LE""
  },
  ""ZMW"": {
    ""title"": ""Квача (замбийская квача)"",
    ""symbol"": ""K""
  },
  ""MAD"": {
    ""title"": ""Дирхам (марокканский дирхам)"",
    ""symbol"": "".د.م • Dh""
  },
  ""ZWL"": {
    ""title"": ""Доллар (доллар Зимбабве)"",
    ""symbol"": ""Z$""
  },
  ""ILS"": {
    ""title"": ""Шекель (новый израильский шекель)"",
    ""symbol"": ""₪""
  },
  ""INR"": {
    ""title"": ""Рупия (индийская рупия)"",
    ""symbol"": ""₹""
  },
  ""IDR"": {
    ""title"": ""Рупия (индонезийская рупия)"",
    ""symbol"": ""Rp""
  },
  ""JOD"": {
    ""title"": ""Динар (иорданский динар)"",
    ""symbol"": "".د.إ • JD""
  },
  ""IQD"": {
    ""title"": ""Динар (иракский динар)"",
    ""symbol"": "".د.ع • ID""
  },
  ""IRR"": {
    ""title"": ""Риал (иранский риал)"",
    ""symbol"": ""﷼ • IR""
  },
  ""ISK"": {
    ""title"": ""Крона (исландская крона)"",
    ""symbol"": ""kr""
  },
  ""YER"": {
    ""title"": ""Риал (йеменский риал)"",
    ""symbol"": ""﷼ • YR""
  },
  ""CVE"": {
    ""title"": ""Эскудо (эскудо Кабо-Верде)"",
    ""symbol"": ""$""
  },
  ""KZT"": {
    ""title"": ""Тенге"",
    ""symbol"": ""₸""
  },
  ""KYD"": {
    ""title"": ""Доллар (доллар Островов Кайман)"",
    ""symbol"": ""$""
  },
  ""KHR"": {
    ""title"": ""Риель"",
    ""symbol"": ""៛""
  },
  ""CAD"": {
    ""title"": ""Доллар (канадский доллар)"",
    ""symbol"": ""$""
  },
  ""QAR"": {
    ""title"": ""Риал (катарский риал)"",
    ""symbol"": ""﷼ • QR""
  },
  ""KES"": {
    ""title"": ""Шиллинг (кенийский шиллинг)"",
    ""symbol"": ""KSh""
  },
  ""KGS"": {
    ""title"": ""Сом"",
    ""symbol"": ""с""
  },
  ""CNY"": {
    ""title"": ""Юань"",
    ""symbol"": ""¥""
  },
  ""COP"": {
    ""title"": ""Песо (колумбийское песо)"",
    ""symbol"": ""$""
  },
  ""KMF"": {
    ""title"": ""Франк (франк Комор)"",
    ""symbol"": ""₣""
  },
  ""CDF"": {
    ""title"": ""Франк (конголезский франк)"",
    ""symbol"": ""₣""
  },
  ""KPW"": {
    ""title"": ""Вона (северокорейская вона)"",
    ""symbol"": ""원""
  },
  ""KRW"": {
    ""title"": ""Вона (южнокорейская вона)"",
    ""symbol"": ""₩""
  },
  ""CRC"": {
    ""title"": ""Колон (коста-риканский колон)"",
    ""symbol"": ""₡""
  },
  ""CUP"": {
    ""title"": ""Песо (кубинское песо)"",
    ""symbol"": ""$""
  },
  ""KWD"": {
    ""title"": ""Динар (кувейтский динар)"",
    ""symbol"": "".د.ك • KD""
  },
  ""ANG"": {
    ""title"": ""Гульден (нидерландский антильский гульден)"",
    ""symbol"": ""ƒ""
  },
  ""LAK"": {
    ""title"": ""Кип"",
    ""symbol"": ""₭""
  },
  ""LSL"": {
    ""title"": ""Лоти (мн. Малоти)"",
    ""symbol"": ""L""
  },
  ""LRD"": {
    ""title"": ""Доллар (либерийский доллар)"",
    ""symbol"": ""$""
  },
  ""LBP"": {
    ""title"": ""Фунт (ливанский фунт)"",
    ""symbol"": "".ل.ل""
  },
  ""LYD"": {
    ""title"": ""Динар (ливийский динар)"",
    ""symbol"": "".د.ل • LD""
  },
  ""CHF"": {
    ""title"": ""Франк (швейцарский франк)"",
    ""symbol"": ""₣""
  },
  ""MUR"": {
    ""title"": ""Рупия (маврикийская рупия)"",
    ""symbol"": ""Re (мн. Rs)""
  },
  ""MRU"": {
    ""title"": ""Угия"",
    ""symbol"": ""UM""
  },
  ""MGA"": {
    ""title"": ""Ариари (малагасийский ариари)"",
    ""symbol"": ""Ar.""
  },
  ""MOP"": {
    ""title"": ""Патака"",
    ""symbol"": ""$""
  },
  ""MKD"": {
    ""title"": ""Денар"",
    ""symbol"": ""ден.""
  },
  ""MWK"": {
    ""title"": ""Квача"",
    ""symbol"": ""K""
  },
  ""MYR"": {
    ""title"": ""Ринггит (малайзийский ринггит)"",
    ""symbol"": ""RM""
  },
  ""MVR"": {
    ""title"": ""Руфия"",
    ""symbol"": "".ރ • Rf""
  },
  ""MXN"": {
    ""title"": ""Песо (мексиканское песо)"",
    ""symbol"": ""$""
  },
  ""MZN"": {
    ""title"": ""Метикал (мозамбикский метикал)"",
    ""symbol"": ""MT""
  },
  ""MDL"": {
    ""title"": ""Лей (молдавский лей)"",
    ""symbol"": ""L""
  },
  ""MNT"": {
    ""title"": ""Тугрик"",
    ""symbol"": ""₮""
  },
  ""MMK"": {
    ""title"": ""Кьят[24]"",
    ""symbol"": ""K""
  },
  ""NAD"": {
    ""title"": ""Доллар (доллар Намибии)"",
    ""symbol"": ""N$""
  },
  ""NPR"": {
    ""title"": ""Рупия (непальская рупия)"",
    ""symbol"": ""Re (мн. Rs)""
  },
  ""NGN"": {
    ""title"": ""Найра"",
    ""symbol"": ""₦""
  },
  ""NIO"": {
    ""title"": ""Кордоба (золотая кордоба)"",
    ""symbol"": ""$""
  },
  ""NZD"": {
    ""title"": ""Доллар (новозеландский доллар)"",
    ""symbol"": ""$""
  },
  ""XPF"": {
    ""title"": ""Франк (франк КФП)"",
    ""symbol"": ""₣""
  },
  ""NOK"": {
    ""title"": ""Крона (норвежская крона)"",
    ""symbol"": ""kr""
  },
  ""AED"": {
    ""title"": ""Дирхам (дирхам ОАЭ)"",
    ""symbol"": "".د.إ • Dh""
  },
  ""OMR"": {
    ""title"": ""Риал (оманский риал)"",
    ""symbol"": ""﷼ • RO""
  },
  ""PKR"": {
    ""title"": ""Рупия (пакистанская рупия)"",
    ""symbol"": ""Re (мн. Rs)""
  },
  ""PAB"": {
    ""title"": ""Бальбоа[14]"",
    ""symbol"": ""B/.""
  },
  ""PGK"": {
    ""title"": ""Кина"",
    ""symbol"": ""K""
  },
  ""PYG"": {
    ""title"": ""Гуарани"",
    ""symbol"": ""₲""
  },
  ""PEN"": {
    ""title"": ""Соль (новый соль)"",
    ""symbol"": ""S/""
  },
  ""PLN"": {
    ""title"": ""Злотый"",
    ""symbol"": ""zł""
  },
  ""RUB"": {
    ""title"": ""Рубль (российский рубль)"",
    ""symbol"": ""₽""
  },
  ""RWF"": {
    ""title"": ""Франк (франк Руанды)"",
    ""symbol"": ""₣""
  },
  ""RON"": {
    ""title"": ""Лей (румынский лей)"",
    ""symbol"": ""L""
  },
  ""SVC"": {
    ""title"": ""Колон (сальвадорский колон)"",
    ""symbol"": ""₡""
  },
  ""WST"": {
    ""title"": ""Тала"",
    ""symbol"": ""$""
  },
  ""STN"": {
    ""title"": ""Добра"",
    ""symbol"": ""Db""
  },
  ""SAR"": {
    ""title"": ""Риял (саудовский риял)"",
    ""symbol"": ""﷼ • SR""
  },
  ""SHP"": {
    ""title"": ""Фунт (фунт Святой Елены)"",
    ""symbol"": ""£""
  },
  ""SCR"": {
    ""title"": ""Рупия (сейшельская рупия)"",
    ""symbol"": ""Re (мн. Rs)""
  },
  ""RSD"": {
    ""title"": ""Динар (сербский динар)"",
    ""symbol"": ""din. • дин.""
  },
  ""SGD"": {
    ""title"": ""Доллар (сингапурский доллар)"",
    ""symbol"": ""$""
  },
  ""SYP"": {
    ""title"": ""Фунт (сирийский фунт)"",
    ""symbol"": "".ل.س • S£""
  },
  ""SBD"": {
    ""title"": ""Доллар (доллар Соломоновых Островов)"",
    ""symbol"": ""$""
  },
  ""SOS"": {
    ""title"": ""Шиллинг (сомалийский шиллинг)"",
    ""symbol"": ""S""
  },
  ""SDG"": {
    ""title"": ""Фунт (суданский фунт)"",
    ""symbol"": ""£""
  },
  ""SRD"": {
    ""title"": ""Доллар (суринамский доллар)"",
    ""symbol"": ""$""
  },
  ""SLL"": {
    ""title"": ""Леоне"",
    ""symbol"": ""Le""
  },
  ""TJS"": {
    ""title"": ""Сомони"",
    ""symbol"": ""с.""
  },
  ""THB"": {
    ""title"": ""Бат"",
    ""symbol"": ""฿""
  },
  ""TWD"": {
    ""title"": ""Доллар (новый тайваньский доллар)"",
    ""symbol"": ""NT$""
  },
  ""TZS"": {
    ""title"": ""Шиллинг (танзанийский шиллинг)"",
    ""symbol"": ""TSh""
  },
  ""TOP"": {
    ""title"": ""Паанга[30]"",
    ""symbol"": ""$""
  },
  ""TTD"": {
    ""title"": ""Доллар (доллар Тринидада и Тобаго)"",
    ""symbol"": ""$""
  },
  ""TND"": {
    ""title"": ""Динар (тунисский динар)"",
    ""symbol"": "".د.ت • TD""
  },
  ""TMT"": {
    ""title"": ""Манат (новый туркменский манат)"",
    ""symbol"": ""m""
  },
  ""TRY"": {
    ""title"": ""Лира (турецкая лира)"",
    ""symbol"": ""₺""
  },
  ""UGX"": {
    ""title"": ""Шиллинг (угандийский шиллинг)"",
    ""symbol"": ""USh""
  },
  ""UZS"": {
    ""title"": ""Сум (узбекский сум)"",
    ""symbol"": ""so’m • сўм""
  },
  ""UAH"": {
    ""title"": ""Гривна"",
    ""symbol"": ""₴""
  },
  ""UYU"": {
    ""title"": ""Песо (уругвайское песо)"",
    ""symbol"": ""$""
  },
  ""FJD"": {
    ""title"": ""Доллар (доллар Фиджи)"",
    ""symbol"": ""$""
  },
  ""PHP"": {
    ""title"": ""Песо (филиппинское песо)"",
    ""symbol"": ""₱""
  },
  ""FKP"": {
    ""title"": ""Фунт (фунт Фолклендских островов)"",
    ""symbol"": ""£""
  },
  ""CZK"": {
    ""title"": ""Крона (чешская крона)"",
    ""symbol"": ""Kč""
  },
  ""CLP"": {
    ""title"": ""Песо (чилийское песо)"",
    ""symbol"": ""$""
  },
  ""SEK"": {
    ""title"": ""Крона (шведская крона)"",
    ""symbol"": ""kr""
  },
  ""LKR"": {
    ""title"": ""Рупия (шри-ланкийская рупия)"",
    ""symbol"": ""Re (мн. Rs)""
  },
  ""ERN"": {
    ""title"": ""Накфа"",
    ""symbol"": ""Nfk""
  },
  ""SZL"": {
    ""title"": ""Лилангени (мн. Эмалангени)"",
    ""symbol"": ""L""
  },
  ""ETB"": {
    ""title"": ""Быр (эфиопский быр)"",
    ""symbol"": ""Br""
  },
  ""ZAR"": {
    ""title"": ""Рэнд"",
    ""symbol"": ""R""
  },
  ""SSP"": {
    ""title"": ""Фунт (южносуданский фунт)"",
    ""symbol"": ""SSP""
  },
  ""JMD"": {
    ""title"": ""Доллар (ямайский доллар)"",
    ""symbol"": ""$""
  },
  ""JPY"": {
    ""title"": ""Иена"",
    ""symbol"": ""¥""
  }
}";
}