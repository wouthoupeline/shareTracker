using ShareTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShareTracker.Application.DTOs;

public class UpdateSecurityRequest
{
    [Required]
    public string Ticker { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Currency { get; set; } = string.Empty;
    public string? Exchange { get; set; }
    public SecurityType Type { get; set; }
}
