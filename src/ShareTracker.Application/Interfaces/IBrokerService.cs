using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface IBrokerService
{
    Task<Guid> CreateAsync(CreateBrokerRequest request);
}