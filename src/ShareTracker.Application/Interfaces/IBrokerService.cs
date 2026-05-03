using ShareTracker.Application.DTOs;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Interfaces;

public interface IBrokerService
{
    Task<Guid> CreateAsync(CreateBrokerRequest request);
    Task<IEnumerable<BrokerResponse>> GetAllAsync();
    Task<BrokerResponse?> GetByIdAsync(Guid id);
    Task<DeleteResult> DeleteAsync(Guid id);
}