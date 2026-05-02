using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Interfaces;

public interface IBrokerRepository
{
    Task AddAsync(Broker broker);
    Task<IEnumerable<Broker>> GetAllAsync();
    Task<Broker?> GetByIdAsync(Guid id);
}