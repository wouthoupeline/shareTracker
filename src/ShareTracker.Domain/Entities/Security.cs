namespace ShareTracker.Domain.Entities;
using ShareTracker.Domain.Enums;
public class Security
{
    public Guid Id {get; set;}
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? Exchange { get; set; }
    public SecurityType Type { get; set; }

}