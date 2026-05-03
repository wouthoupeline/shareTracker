using Microsoft.EntityFrameworkCore;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Infrastructure.Persistence;

namespace ShareTracker.Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly ShareTrackerDbContext _context;

    public PurchaseRepository(ShareTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _context.Purchases.AddAsync(purchase);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await  _context.Purchases
                            .Include(p => p.Security)
                            .Include(p => p.Broker)
                            .ToListAsync();
    }

    public async Task<Purchase?> GetByIdAsync(Guid id)
    {
        return await _context.Purchases
                            .Include(p => p.Security)
                            .Include(p => p.Broker)
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeleteAsync(Purchase purchase)
    {
        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();
    }
}