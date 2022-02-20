using App.Models.Entry;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class EntryController : Controller
{
    private readonly ApplicationContext _db;
    private readonly EntryService _entryService;

    public EntryController(
        ApplicationContext db,
        EntryService entryService
    )
    {
        _db = db;
        _entryService = entryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<BaseEntry>>> Get()
    {
        return await _db.Entries.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BaseEntry>> Get(Guid id)
    {
        var entry = await _db.Entries.FindAsync(id);
        if (entry == null)
        {
            return NotFound();
        }

        return entry;
    }

    [HttpPost("person")]
    public async Task<ActionResult<Person>> CreatePerson(PersonDto dto)
    {
        var person = _entryService.Create(dto);
        _db.Persons.Add(person);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new {id = person.Id}, person);
    }
    
    [HttpPost("company")]
    public async Task<ActionResult<Company>> CreateCompany(CompanyDto dto)
    {
        var company = _entryService.Create(dto);
        _db.Companies.Add(company);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new {id = company.Id}, company);
    }
    
    [HttpPost("meet")]
    public async Task<ActionResult<Meet>> CreateMeet(MeetDto dto)
    {
        var meet = _entryService.Create(dto);
        _db.Meets.Add(meet);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new {id = meet.Id}, meet);
    }
}