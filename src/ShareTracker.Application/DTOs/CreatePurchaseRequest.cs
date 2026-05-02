using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.DTOs;

public class CreatePurchaseRequest
{
    public Guid SecurityId { get; set; }
    public Guid BrokerId { get; set; }
    public DateTime Date { get; set; }
    public decimal PricePerShare { get; set; }
    public decimal Quantity { get; set; }
}
