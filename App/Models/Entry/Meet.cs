namespace App.Models.Entry;

public class Meet: BaseEntry
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public string Location { get; set; } = null!;
}

public class MeetDto : BaseEntryDto
{
    public string StartAt { get; set; }
    public string EndAt { get; set; }
    public string Location { get; set; } = null!;
}