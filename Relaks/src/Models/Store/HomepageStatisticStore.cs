using BootstrapBlazor.Components;
using Relaks.Database;

namespace Relaks.Models.Store;

public class HomepageStatisticStore
{
    private readonly AppDbContext _db;

    public HomepageStatisticStore(AppDbContext db)
    {
        _db = db;
    }

    public Dictionary<string, int> EntryRelationships { get; set; } = new();
    public Dictionary<string, int> FinancialCounts { get; set; } = new();
    public Dictionary<string, int> EntryCounts { get; set; } = new();
    public Dictionary<string, int> FileCounts { get; set; } = new();
    public Dictionary<string, int> EInfoCounts { get; set; } = new();

    public void FindData()
    {
        EntryRelationships["Метки объединений"] = _db.EntryTagTitles.Count();
        EntryRelationships["Категории меток объединений"] = _db.EntryTagCategories.Count();
        EntryRelationships["Взаимосвязи с метками"] = _db.EntryTags.Count();
        EntryRelationships["Взаимоотношения объединений"] = _db.EntryRelations.Count();
        
        EntryCounts["Люди"] = _db.EPersons.Count();
        EntryCounts["Компании"] = _db.ECompanies.Count();
        EntryCounts["Проекты"] = _db.EProjects.Count();
        
        FileCounts["Файлы"] = _db.EntryFiles.Count();
        FileCounts["Метки файлов"] = _db.EntryFileTags.Count();
        FileCounts["Категории файлов"] = _db.EntryFileCategories.Count();
        FileCounts["Общие файлы"] = _db.BaseFiles.Count(x => x.BaseEntryRelations.Any());
        
        EInfoCounts["Телефоны"] = _db.EiPhones.Count();
        EInfoCounts["Даты"] = _db.EiDates.Count();
        EInfoCounts["Эл. почты"] = _db.EiEmails.Count();
        EInfoCounts["Ссылки"] = _db.EiUrls.Count();
        EInfoCounts["Наборы данных"] = _db.EiDatasets.Count();

        FinancialCounts["Финансовые транзакции"] = _db.EntryFinancialTransactions.Count();
        FinancialCounts["Категории финансовых транзакций"] = _db.FinancialTransactionCategories.Count();
        FinancialCounts["Финансовые счета"] = _db.FinancialAccounts.Count();
        FinancialCounts["Категории финансовых счетов"] = _db.FinancialAccountCategories.Count();
    }
}