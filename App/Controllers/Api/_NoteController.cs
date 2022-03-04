// using App.Mappers;
// using App.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Query;
// using Microsoft.EntityFrameworkCore;
//
// namespace App.Controllers.Api;
//
// [ApiController]
// [Route("api/notes")]
// public class NoteController : BaseApiController<Note, NoteCreateDto>
// {
//     public NoteController(ApplicationContext db) : base(db)
//     {
//     }
//
//     [HttpPost]
//     public override async Task<ActionResult<Note>> Create(NoteCreateDto createDto)
//     {
//         var note = new Note();
//         createDto.MapTo(note);
//         QueryEntities().Add(note);
//         await Db.SaveChangesAsync();
//         
//         return CreatedAtAction(nameof(Get), new {id = note.Id}, note);
//     }
//
//     [HttpPatch("{id:guid}")]
//     public async Task<ActionResult> Patch(Guid id, NotePatchDto patchDto)
//     {
//         return Ok(new
//         {
//             State = ModelState.IsValid
//         });
//         // var note = await GetEntity(id);
//         // if (note == null)
//         // {
//         //     return NotFound();
//         // }
//         //
//         // patchDto.MapTo(note);
//         // await Db.SaveChangesAsync();
//         //
//         // return NoContent();
//     }
//     
//     public override DbSet<Note> QueryEntities()
//     {
//         return Db.Notes;
//     }
//     
//     [HttpGet("{id:guid}")]
//     public override async Task<ActionResult<Note?>> Get(Guid id)
//     {
//         return await base.Get(id);
//     }
//     
//     [HttpGet]
//     [EnableQuery]
//     public override IEnumerable<Note> GetAll()
//     {
//         return base.GetAll();
//     }
//     
//     [HttpDelete("{id:guid}")]
//     public override async Task<ActionResult> Delete(Guid id)
//     {
//         return await base.Delete(id);
//     }
// }