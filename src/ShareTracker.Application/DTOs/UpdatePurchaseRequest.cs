using System.ComponentModel.DataAnnotations;

namespace ShareTracker.Application.DTOs;

public class UpdatePurchaseRequest
{
    [Required]
    public Guid? SecurityId { get; set; }
    [Required]
    public Guid? BrokerId { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal PricePerShare { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal Quantity { get; set; }
}
