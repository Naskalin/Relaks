using App.Models.Entry;

namespace App.Services;

public class EntryService
{
    // private readonly ApplicationContext _db;
    //
    // public EntryService(ApplicationContext db)
    // {
    //     _db = db;
    // }

    public Person Create(PersonDto dto)
    {
        var person = new Person()
        {
            Name = dto.Name,
            Reputation = dto.Reputation,
        };

        if (dto.BirthDay != null)
        {
            person.BirthDay = Convert.ToDateTime(dto.BirthDay);
        }

        return person;
    }

    public Company Create(CompanyDto dto)
    {
        var company = new Company()
        {
            Name = dto.Name,
            Reputation = dto.Reputation,
        };

        return company;
    }

    public Meet Create(MeetDto dto)
    {
        var meet = new Meet()
        {
            Name = dto.Name,
            Reputation = dto.Reputation,
            Location = dto.Location,
            StartAt = Convert.ToDateTime(dto.StartAt),
            EndAt = Convert.ToDateTime(dto.EndAt),
        };

        return meet;
    }
}