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
            SecurityId = request.SecurityId,
            BrokerId = request.BrokerId,
            Date  = request.Date,
            PricePerShare = request.PricePerShare,
            Quantity = request.Quantity,
        };

        await _repository.AddAsync(purchase);
        return purchase.Id;
    }

    public async Task<IEnumerable<PurchaseResponse>> GetAllAsync()
    {
        var securities = await _repository.GetAllAsync();
        return securities.Select(s => new PurchaseResponse
        {
            Id = s.Id,
            SecurityId = s.SecurityId,
            BrokerId = s.BrokerId,
            Date = s.Date,
            PricePerShare = s.PricePerShare,
            Quantity = s.Quantity
        });
    }

    public async Task<PurchaseResponse?> GetByIdAsync(Guid id)
    {
        var purchase = await _repository.GetByIdAsync(id);
        if (purchase == null) return null;

        return new PurchaseResponse
        {
            Id = purchase.Id,
            SecurityId = purchase.SecurityId,
            BrokerId = purchase.BrokerId,
            Date = purchase.Date,
            PricePerShare = purchase.PricePerShare,
            Quantity = purchase.Quantity
        };
    }
}