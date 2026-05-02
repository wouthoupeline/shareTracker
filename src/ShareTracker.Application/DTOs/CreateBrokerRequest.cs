using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.DTOs;

public class CreateBrokerRequest
{
    public string Name { get; set; } = string.Empty;
}