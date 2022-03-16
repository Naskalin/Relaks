using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;
using App.Utils.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class EntryRepository : BaseRepository
{
    public EntryRepository(AppDbContext db) : base(db)
    {
    }
    
    public async Task<IEnumerable<Entry>> GetAllAsync(ListRequest listRequest)
    {
        var perPage = listRequest.PerPage ?? 50;
        var page = listRequest.Page ?? 1;

        var query = Db.Entries.Take(perPage).Skip(perPage * (page - 1));

        if (listRequest.EntryType != null)
        {
            query = query.Where(x => x.EntryType == listRequest.EntryType);
        }

        if (listRequest.Search != null)
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            query = query.Where(x =>
                EF.Functions.Like(x.Name, "%" + listRequest.Search + "%")
                || (x.Description != null && EF.Functions.Like(x.Description, "%" + listRequest.Search + "%"))
            );
        }

        if (listRequest.OrderBy != null)
        {
            query = query.OrderBy(listRequest.OrderBy, listRequest.OrderByDesc ?? false);
        }

        query = query.OrderByDescending(x => x.UpdatedAt);

        return await query.ToListAsync();
    }

    public async Task<Entry?> FindSingleAsync(Guid id)
    {
        return await Db.Entries.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Create(Entry entry)
    {
        Db.Entries.Add(entry);
    }

    public void Update(Entry entry)
    {
        Db.Entry(entry).State = EntityState.Modified;
    }
}