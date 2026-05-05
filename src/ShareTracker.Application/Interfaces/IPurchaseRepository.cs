using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Interfaces;

public interface IPurchaseRepository
{
    Task AddAsync(Purchase purchase);
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase?> GetByIdAsync(Guid id);
    Task DeleteAsync(Purchase purchase);
    Task SaveAsync();
}