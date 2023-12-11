using Microsoft.AspNetCore.Components;

namespace Relaks.Interfaces;

public interface IGuidRowView
{
    public RenderFragment Th { get; set; }
    public RenderFragment<Guid> Td { get; set; }
}

public class GuidRowView : IGuidRowView
{
    public RenderFragment Th { get; set; } = null!;
    public RenderFragment<Guid> Td { get; set; } = null!;
}