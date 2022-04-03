// using App.DbConfigurations;
// using App.Endpoints.Entries.Texts;
// using App.Models;
// using Microsoft.EntityFrameworkCore;
// using App.Utils.Extensions.Database;
//
// namespace App.Repository;
//
// public class EntryTextRepository : BaseRepository<EntryInfo>
// {
//     public EntryTextRepository(AppDbContext db) : base(db)
//     {
//     }
//
//     public async Task<List<EntryInfo>> PaginateListAsync(ListRequest request, CancellationToken cancellationToken)
//     {
//         Enum.TryParse(request.TextType, true, out TextTypeEnum textTypeEnum);
//
//         var query = Entities
//             .Where(x => x.EntryId == request.EntryId)
//             .Where(x => x.TextType == textTypeEnum);
//
//         if (request.Search != null)
//         {
//             query = query.Where(x => 
//                     EF.Functions.Like(x.Title, "%" + request.Search + "%")
//                     || EF.Functions.Like(x.Val, "%" + request.Search + "%")
//                     )
//                 ;
//         }
//         
//         if (request.OrderBy != null)
//         {
//             query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
//         }
//
//         query = query.OrderByDescending(x => x.UpdatedAt);
//
//         query = PaginateQuery(query, request);
//         return await query.ToListAsync(cancellationToken);
//     }
// }