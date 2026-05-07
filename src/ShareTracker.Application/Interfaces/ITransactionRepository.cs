using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(Guid id);
    Task DeleteAsync(Transaction transaction);
    Task SaveAsync();
}
