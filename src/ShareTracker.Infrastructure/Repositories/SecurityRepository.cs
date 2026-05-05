using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Infrastructure.Persistence;

namespace ShareTracker.Infrastructure.Repositories;

public class SecurityRepository : ISecurityRepository
{
    private readonly ShareTrackerDbContext _context;

    public SecurityRepository(ShareTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Security security)
    {
        await _context.Securities.AddAsync(security);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Security>> GetAllAsync()
    {
        return await _context.Securities.ToListAsync();
    }

    public async Task<Security?> GetByIdAsync(Guid id)
    {
        return await _context.Securities.FindAsync(id);
    }

    public async Task DeleteAsync(Security security)
    {
        _context.Securities.Remove(security);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasPurchasesAsync(Guid id)
    {
        return await _context.Purchases.AnyAsync(p => p.SecurityId == id);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}