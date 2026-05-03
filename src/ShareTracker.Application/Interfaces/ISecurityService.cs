using ShareTracker.Application.DTOs;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Interfaces;

public interface ISecurityService
{
    Task<Guid> CreateAsync(CreateSecurityRequest request);
    Task<IEnumerable<SecurityResponse>> GetAllAsync();
    Task<SecurityResponse?> GetByIdAsync(Guid id);
    Task<DeleteResult> DeleteAsync(Guid id);
}