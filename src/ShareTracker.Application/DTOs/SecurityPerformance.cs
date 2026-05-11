namespace ShareTracker.Application.DTOs;

public class SecurityPerformance
{
    public Guid SecurityId { get; set; }
    public string SecurityTicker { get; set; } = string.Empty;
    public decimal TotalInvested { get; set; }
    public decimal CurrentValue { get; set; }
    public string Currency { get; set; } = string.Empty;
}
