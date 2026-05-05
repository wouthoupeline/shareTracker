using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Services;

public class SecurityService : ISecurityService
{
    private readonly ISecurityRepository _repository;

    public SecurityService(ISecurityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(CreateSecurityRequest request)
    {
        var security = new Security
        {
            Id = Guid.NewGuid(),
            Ticker = request.Ticker,
            Name = request.Name,
            Type = request.Type,
            Exchange = request.Exchange,
            Currency = request.Currency
        };

        await _repository.AddAsync(security);
        return security.Id;
    }

   public async Task<IEnumerable<SecurityResponse>> GetAllAsync()
    {
        var securities = await _repository.GetAllAsync();
        return securities.Select(s => new SecurityResponse
        {
            Id = s.Id,
            Ticker = s.Ticker,
            Name = s.Name,
            Type = s.Type,
            Exchange = s.Exchange,
            Currency = s.Currency
        });
    }

    public async Task<SecurityResponse?> GetByIdAsync(Guid id)
    {
        var security = await _repository.GetByIdAsync(id);
        if (security == null) return null;

        return new SecurityResponse
        {
            Id = security.Id,
            Ticker = security.Ticker,
            Name = security.Name,
            Type = security.Type,
            Exchange = security.Exchange,
            Currency = security.Currency
        };
    }

    public async Task<(UpdateResult Result, SecurityResponse? Data)> UpdateAsync(Guid id, UpdateSecurityRequest request)
    {
        var security = await _repository.GetByIdAsync(id);
        if (security == null) return (UpdateResult.NotFound, null);

        security.Ticker = request.Ticker;
        security.Name = request.Name;
        security.Type = request.Type;
        security.Exchange = request.Exchange;
        security.Currency = request.Currency;

        await _repository.SaveAsync();

        return (UpdateResult.Success, new SecurityResponse
        {
            Id = security.Id,
            Ticker = security.Ticker,
            Name = security.Name,
            Type = security.Type,
            Exchange = security.Exchange,
            Currency = security.Currency
        });
    }

    public async Task<DeleteResult> DeleteAsync(Guid id)
    {
        var security = await _repository.GetByIdAsync(id);
        if (security == null) return DeleteResult.NotFound;

        var hasPurchases = await _repository.HasPurchasesAsync(id);
        if (hasPurchases) return DeleteResult.Conflict;

        await _repository.DeleteAsync(security);
        return DeleteResult.Success;
    }
}