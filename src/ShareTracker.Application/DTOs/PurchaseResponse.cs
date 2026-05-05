namespace ShareTracker.Application.DTOs;

public class PurchaseResponse
{
    public Guid Id { get; set; }
    public Guid SecurityId { get; set; }
    public Guid BrokerId { get; set; }
    public string BrokerName { get; set; } = string.Empty;
    public string SecurityTicker { get; set; } = string.Empty;
    public string SecurityName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal PricePerShare { get; set; }
    public decimal Quantity { get; set; }
}