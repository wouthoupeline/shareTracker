using ShareTracker.Domain.Enums;

namespace ShareTracker.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid SecurityId { get; set; }
    public Security Security { get; set; } = null!;
    public Guid BrokerId { get; set; }
    public Broker Broker { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal PricePerShare { get; set; }
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
}
