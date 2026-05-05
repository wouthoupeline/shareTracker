using System.ComponentModel.DataAnnotations;

namespace ShareTracker.Application.DTOs;

public class UpdateBrokerRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
