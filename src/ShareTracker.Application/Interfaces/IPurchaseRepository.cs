using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Interfaces;

public interface IPurchaseRepository
{
    Task AddAsync(Purchase purchase);
}