using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface IPricingService
{
    Task<PriceResult?> GetCurrentPriceAsync(string ticker);
}