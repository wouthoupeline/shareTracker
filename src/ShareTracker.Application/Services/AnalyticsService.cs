using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPricingService _pricingService;

    public AnalyticsService(ITransactionRepository transactionRepository, IPricingService pricingService)
    {
        _transactionRepository = transactionRepository;
        _pricingService = pricingService;
    }

    public async Task<IEnumerable<SecurityPerformance>> GetPerformanceAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();

        var results = new List<SecurityPerformance>();

        foreach (var group in transactions.GroupBy(t => t.SecurityId))
        {
            var security = group.First().Security;

            var totalInvested = group.Sum(t => t.Type == TransactionType.Buy
                ? t.PricePerShare * t.Quantity
                : -(t.PricePerShare * t.Quantity));

            var netShares = group.Sum(t => t.Type == TransactionType.Buy
                ? t.Quantity
                : -t.Quantity);

            var priceResult = await _pricingService.GetCurrentPriceAsync(security.Ticker);

            results.Add(new SecurityPerformance
            {
                SecurityId = security.Id,
                SecurityTicker = security.Ticker,
                TotalInvested = totalInvested,
                CurrentValue = priceResult != null ? priceResult.Price * netShares : 0,
                Currency = priceResult?.Currency ?? security.Currency,
            });
        }

        return results;
    }
}
