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
}