using Relaks.Interfaces;

namespace Relaks.Models.Misc;

public class MyPhone : IPhoneWithRegion
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public MyPhone(string number, string region)
    {
        Number = number;
        Region = region;
    }
}