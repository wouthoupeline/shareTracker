using ShareTracker.Application.DTOs;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Interfaces;

public interface IPurchaseService
{
    Task<Guid> CreateAsync(CreatePurchaseRequest request);
    Task<IEnumerable<PurchaseResponse>> GetAllAsync();
    Task<PurchaseResponse?> GetByIdAsync(Guid id);
    Task<DeleteResult> DeleteAsync(Guid id);
    Task<(UpdateResult Result, PurchaseResponse? Data)> UpdateAsync(Guid id, UpdatePurchaseRequest request);
}