using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface IPurchaseService
{
    Task<Guid> CreateAsync(CreatePurchaseRequest request);
    Task<IEnumerable<PurchaseResponse>> GetAllAsync();
    Task<PurchaseResponse?> GetByIdAsync(Guid id);
}