using System.ComponentModel.DataAnnotations;

namespace ShareTracker.Application.DTOs;

public class CreateBrokerRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}