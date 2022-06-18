using App.DbConfigurations;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class StructureItemRepository : BaseRepository<StructureItem>
{
    public StructureItemRepository(AppDbContext db) : base(db)
    {
    }
    
    public async Task<StructureItem?> FindByIdWithRelationsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities
            .Include(x => x.Entry)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}