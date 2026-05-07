using System.ComponentModel.DataAnnotations;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.DTOs;

public class UpdateTransactionRequest
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
    [Required]
    public TransactionType? Type { get; set; }
}
