using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface ISecurityService
{
    Task<Guid> CreateAsync(CreateSecurityRequest request);
    Task<IEnumerable<SecurityResponse>> GetAllAsync();
    Task<SecurityResponse?> GetByIdAsync(Guid id);
}