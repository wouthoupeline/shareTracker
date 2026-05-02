using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface IPurchaseService
{
    Task<Guid> CreateAsync(CreatePurchaseRequest request);
}