namespace ShareTracker.Application.DTOs;

public class PriceResult
{
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
}