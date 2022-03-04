// using App.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Routing.Controllers;
// using Microsoft.EntityFrameworkCore;
//
// namespace App.Controllers.Api;
//
// public abstract class BaseApiController<T, TDto> : ODataController 
//     where T : class, IGuidId
// {
//     protected readonly ApplicationContext Db;
//
//     protected BaseApiController(ApplicationContext db)
//     {
//         Db = db;
//     }
//     
//     public abstract Task<ActionResult<T>> Create(TDto dto);
//     public abstract DbSet<T> QueryEntities();
//
//     public virtual IEnumerable<T> GetAll()
//     {
//         return QueryEntities();
//     }
//     
//     // Get single
//     public virtual async Task<ActionResult<T?>> Get(Guid id)
//     {
//         var entity = await GetEntity(id);
//         if (entity == null)
//         {
//             return NotFound();
//         }
//
//         return entity;
//     }
//     
//     // public abstract Task<ActionResult<T>> Patch(Guid id, T entry);
//
//     protected async Task<T?> GetEntity(Guid id)
//     {
//         return await QueryEntities().FirstOrDefaultAsync(x => x.Id == id);
//     }
//     
//     public virtual async Task<ActionResult> Delete(Guid id)
//     {
//         var entry = await GetEntity(id);
//         if (entry == null)
//         {
//             return NotFound();
//         }
//         
//         QueryEntities().Remove(entry);
//         await Db.SaveChangesAsync();
//         return NoContent();
//     }
// }