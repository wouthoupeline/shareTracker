using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _repository;
    private readonly ISecurityRepository _securityRepository;
    private readonly IBrokerRepository _brokerRepository;

    public PurchaseService(
        IPurchaseRepository repository,
        ISecurityRepository securityRepository,
        IBrokerRepository brokerRepository)
    {
        _repository = repository;
        _securityRepository = securityRepository;
        _brokerRepository = brokerRepository;
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
            SecurityId = p.SecurityId,
            BrokerId = p.BrokerId,
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
            SecurityId = purchase.SecurityId,
            BrokerId = purchase.BrokerId,
            SecurityTicker = purchase.Security.Ticker,
            SecurityName = purchase.Security.Name,
            BrokerName = purchase.Broker.Name,
            Date = purchase.Date,
            PricePerShare = purchase.PricePerShare,
            Quantity = purchase.Quantity
        };
    }

    public async Task<(UpdateResult Result, PurchaseResponse? Data)> UpdateAsync(Guid id, UpdatePurchaseRequest request)
    {
        var purchase = await _repository.GetByIdAsync(id);
        if (purchase == null) return (UpdateResult.NotFound, null);

        var security = await _securityRepository.GetByIdAsync(request.SecurityId!.Value);
        if (security == null) return (UpdateResult.InvalidReference, null);

        var broker = await _brokerRepository.GetByIdAsync(request.BrokerId!.Value);
        if (broker == null) return (UpdateResult.InvalidReference, null);

        purchase.SecurityId = request.SecurityId!.Value;
        purchase.BrokerId = request.BrokerId!.Value;
        purchase.Date = request.Date!.Value;
        purchase.PricePerShare = request.PricePerShare;
        purchase.Quantity = request.Quantity;

        await _repository.SaveAsync();

        return (UpdateResult.Success, new PurchaseResponse
        {
            Id = purchase.Id,
            SecurityId = purchase.SecurityId,
            BrokerId = purchase.BrokerId,
            SecurityTicker = security.Ticker,
            SecurityName = security.Name,
            BrokerName = broker.Name,
            Date = purchase.Date,
            PricePerShare = purchase.PricePerShare,
            Quantity = purchase.Quantity
        });
    }

    public async Task<DeleteResult> DeleteAsync(Guid id)
    {
        var purchase = await _repository.GetByIdAsync(id);
        if (purchase == null) return DeleteResult.NotFound;

        await _repository.DeleteAsync(purchase);
        return DeleteResult.Success;
    }
}