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
        return await _context.Purchases.ToListAsync();
    }

    public async Task<Purchase?> GetByIdAsync(Guid id)
    {
        return await _context.Purchases.FindAsync(id);
    }
}