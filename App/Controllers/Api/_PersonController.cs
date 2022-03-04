// using App.Mappers;
// using App.Models.Entry;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Query;
// using Microsoft.EntityFrameworkCore;
//
// namespace App.Controllers.Api;
//
// [ApiController]
// [Route("api/[controller]")]
// public sealed class PersonController : BaseApiController<Person, PersonDto>
// {
//     public PersonController(ApplicationContext db) : base(db)
//     {
//     }
//     
//     [HttpPost]
//     public override async Task<ActionResult<Person>> Create(PersonDto dto)
//     {
//         var person = new Person();
//         dto.MapTo(person);
//         Db.Persons.Add(person);
//         await Db.SaveChangesAsync();
//         
//         return CreatedAtAction(nameof(Get), new {id = person.Id}, person);
//     }
//
//     public override DbSet<Person> QueryEntities()
//     {
//         return Db.Persons;
//     }
//
//     [HttpGet]
//     [EnableQuery]
//     public override IEnumerable<Person> GetAll()
//     {
//         return base.GetAll();
//     }
//     
//     [HttpGet("{id:guid}")]
//     public override async Task<ActionResult<Person?>> Get(Guid id)
//     {
//         return await base.Get(id);
//     }
//
//     // [HttpPatch("{id:guid}")]
//     // public async Task<IActionResult> Patch(Guid id, PersonDto dto)
//     // {
//     //     var changed = delta.GetChangedPropertyNames().ToArray();
//     //     
//     //     var person = await GetEntity(id);
//     //     if (person == null)
//     //     {
//     //         return NotFound();
//     //     }
//     //     
//     //     delta.Patch(person);
//     //     await Db.SaveChangesAsync();
//     //     return NoContent();
//     // }
//
//     [HttpDelete("{id:guid}")]
//     public override async Task<ActionResult> Delete(Guid id)
//     {
//         return await base.Delete(id);
//     }
// }