using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;

namespace ShareTracker.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _repository;

    public PurchaseService(IPurchaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(CreatePurchaseRequest request)
    {
        var purchase = new Purchase
        {
            Id = Guid.NewGuid(),
            SecurityId = request.SecurityId!.Value,
            BrokerId = request.BrokerId!.Value,
            Date  = request.Date!.Value,
            PricePerShare = request.PricePerShare,
            Quantity = request.Quantity,
        };

        await _repository.AddAsync(purchase);
        return purchase.Id;
    }

    public async Task<IEnumerable<PurchaseResponse>> GetAllAsync()
    {
        var securities = await _repository.GetAllAsync();
        return securities.Select(p => new PurchaseResponse
        {
            Id = p.Id,
            SecurityTicker = p.Security.Ticker,
            SecurityName = p.Security.Name,
            BrokerName = p.Broker.Name,
            Date = p.Date,
            PricePerShare = p.PricePerShare,
            Quantity = p.Quantity
        });
    }

    public async Task<PurchaseResponse?> GetByIdAsync(Guid id)
    {
        var purchase = await _repository.GetByIdAsync(id);
        if (purchase == null) return null;

        return new PurchaseResponse
        {
            Id = purchase.Id,
            SecurityTicker = purchase.Security.Ticker,
            SecurityName = purchase.Security.Name,
            BrokerName = purchase.Broker.Name,
            Date = purchase.Date,
            PricePerShare = purchase.PricePerShare,
            Quantity = purchase.Quantity
        };
    }
}