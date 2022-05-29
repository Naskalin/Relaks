// using App.Endpoints.Entries.EntryInfos;
// using App.Models;
// using App.Utils.Extensions.Database;
// using Microsoft.EntityFrameworkCore;
//
// namespace App.Repository.EntryInfoExtends;
//
// public static class EmailExtends
// {
//     public static async Task<List<EntryInfo>> EmailsAsync(
//         this EntryInfoRepository repository,
//         EntryInfoListRequest request,
//         CancellationToken cancellationToken
//     )
//     {
//         var query = repository.Entities.Where(x => x.EntryId == request.EntryId);
//
//         if (request.Type != null)
//             query = query.Where(x => x.Type == request.Type);
//
//         if (request.IsDeleted != null)
//             query = query.Where(x => request.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
//         
//         //TODO: Search
//         // if (!string.IsNullOrEmpty(request.Search))
//         //     query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%")
//         //                              || EF.Functions.Like(x.Email, "%" + request.Search + "%")
//         //                              || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
//         //     );
//
//         if (!string.IsNullOrEmpty(request.OrderBy))
//         {
//             query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
//         }
//         else
//         {
//             query = query.OrderByDescending(x => x.UpdatedAt);
//         }
//         
//         if (request.Page != null && request.PerPage != null)
//         {
//             query = repository.PaginateQuery(query, request);     
//         }
//         
//         return await query.ToListAsync(cancellationToken);
//     }
//     
//     //     public async Task<List<EntryEmail>> PaginateListAsync(
//     //     EntryInfoListRequest request,
//     //     CancellationToken cancellationToken)
//     // {
//     //     var query = Entities.Where(x => x.EntryId == request.EntryId);
//     //     
//     //     if (request.isDeleted != null)
//     //         query = query.Where(x => request.isDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
//     //
//     //     if (!string.IsNullOrEmpty(request.Search))
//     //         query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%")
//     //                                  || EF.Functions.Like(x.Email, "%" + request.Search + "%")
//     //                                  || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
//     //         );
//     //
//     //     if (!string.IsNullOrEmpty(request.OrderBy))
//     //     {
//     //         query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
//     //     }
//     //     else
//     //     {
//     //         query = query.OrderByDescending(x => x.UpdatedAt);
//     //     }
//     //
//     //     if (request.Page != null && request.PerPage != null)
//     //     {
//     //         query = PaginateQuery(query, request);     
//     //     }
//     //
//     //     return await query.ToListAsync(cancellationToken);
//     // }
// }