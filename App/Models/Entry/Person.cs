namespace App.Models.Entry;

public class Person : BaseEntry
{
    public DateTime? BirthDay { get; set; }
}

public class PersonDto : BaseEntryDto
{
    public string? BirthDay { get; set; }
}