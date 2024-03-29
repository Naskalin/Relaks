﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    protected readonly AppDbContext Db;
    protected readonly Faker Faker;

    public DatabaseSeeder(AppDbContext db)
    {
        Db = db;
        Faker = new Faker("ru");
    }

    public void SeedAll()
    {
        // clear fts
        Db.Database.ExecuteSqlRaw("DELETE FROM FinancialTransactionItems;");
        Db.Database.ExecuteSqlRaw("DELETE FROM FinancialTransactions;");
        Db.Database.ExecuteSqlRaw("DELETE FROM FinancialTransactionCategories;");
        Db.Database.ExecuteSqlRaw("DELETE FROM FinancialAccounts;");
        Db.Database.ExecuteSqlRaw("DELETE FROM FinancialAccountCategories;");
        
        
        Db.Database.ExecuteSqlRaw("DELETE FROM FtsEntryInfos;");
        Db.Database.ExecuteSqlRaw("DELETE FROM FtsEntries;");
        
        Db.Database.ExecuteSqlRaw("DELETE FROM EntryInfos;");
        Db.Database.ExecuteSqlRaw("DELETE FROM Entries;");
        
        Db.Database.ExecuteSqlRaw("delete from StructureItems;");
        Db.Database.ExecuteSqlRaw("delete from StructureGroups;");
        Db.Database.ExecuteSqlRaw("delete from EntryRelations;");
        
        // SeedProfessions();
        SeedEntries();
        SeedEntryInfos();
        SeedStructures();
        // // SeedInfoTemplate();
        SeedEntryRelations();
        SeedFinancials();
    }
}