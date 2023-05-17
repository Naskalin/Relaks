using Relaks.Interfaces;

namespace Relaks.Models.Misc;

public class MyPhone : IPhone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public MyPhone()
    {
    }

    public MyPhone(string number, string region)
    {
        Number = number;
        Region = region;
    }
}