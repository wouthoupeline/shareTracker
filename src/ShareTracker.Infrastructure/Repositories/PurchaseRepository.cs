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
}