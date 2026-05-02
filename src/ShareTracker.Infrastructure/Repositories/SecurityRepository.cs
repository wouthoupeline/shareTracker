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
}