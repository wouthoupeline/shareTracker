using Microsoft.EntityFrameworkCore;
using ShareTracker.Domain.Entities;

namespace ShareTracker.Infrastructure.Persistence;

public class ShareTrackerDbContext : DbContext
{
    public ShareTrackerDbContext(DbContextOptions<ShareTrackerDbContext> options) : base(options)
    {
    }

    public DbSet<Security> Securities { get; set; }
    public DbSet<Broker> Brokers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}