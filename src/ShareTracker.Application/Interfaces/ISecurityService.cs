using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface ISecurityService
{
    Task<Guid> CreateAsync(CreateSecurityRequest request);
}