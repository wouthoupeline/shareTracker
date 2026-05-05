using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Interfaces;

public interface ISecurityRepository
{
    Task AddAsync(Security security);
    Task<IEnumerable<Security>> GetAllAsync();
    Task<Security?> GetByIdAsync(Guid id);
    Task DeleteAsync(Security security);
    Task<bool> HasPurchasesAsync(Guid id);
    Task SaveAsync();
}