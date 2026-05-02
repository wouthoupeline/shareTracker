namespace ShareTracker.Domain.Entities;

public class Purchase
{
    public Guid Id { get; set; }
    public Guid SecurityId { get; set; }
    public required Security Security { get; set; }
    public Guid BrokerId { get; set; }
    public required Broker Broker { get; set; }
    public DateTime Date { get; set; }
    public decimal PricePerShare { get; set; }
    public decimal Quantity { get; set; }
}