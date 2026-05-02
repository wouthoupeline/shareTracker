using Microsoft.EntityFrameworkCore;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Infrastructure.Persistence;

namespace ShareTracker.Infrastructure.Repositories;

public class BrokerRepository : IBrokerRepository
{
    private readonly ShareTrackerDbContext _context;

    public BrokerRepository(ShareTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Broker broker)
    {
        await _context.Brokers.AddAsync(broker);
        await _context.SaveChangesAsync();
    }

        public async Task<IEnumerable<Broker>> GetAllAsync()
    {
        return await _context.Brokers.ToListAsync();
    }

    public async Task<Broker?> GetByIdAsync(Guid id)
    {
        return await _context.Brokers.FindAsync(id);
    }
}