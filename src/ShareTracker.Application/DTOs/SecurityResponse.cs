using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.DTOs;

public class SecurityResponse
{
    public Guid Id { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? Exchange { get; set; }
    public SecurityType Type { get; set; }
}