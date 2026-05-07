using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ISecurityRepository _securityRepository;
    private readonly IBrokerRepository _brokerRepository;

    public TransactionService(
        ITransactionRepository repository,
        ISecurityRepository securityRepository,
        IBrokerRepository brokerRepository)
    {
        _repository = repository;
        _securityRepository = securityRepository;
        _brokerRepository = brokerRepository;
    }

    public async Task<Guid> CreateAsync(CreateTransactionRequest request)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            SecurityId = request.SecurityId!.Value,
            BrokerId = request.BrokerId!.Value,
            Date = request.Date!.Value,
            PricePerShare = request.PricePerShare,
            Quantity = request.Quantity,
            Type = request.Type!.Value,
        };

        await _repository.AddAsync(transaction);
        return transaction.Id;
    }

    public async Task<IEnumerable<TransactionResponse>> GetAllAsync()
    {
        var transactions = await _repository.GetAllAsync();
        return transactions.Select(t => new TransactionResponse
        {
            Id = t.Id,
            SecurityId = t.SecurityId,
            BrokerId = t.BrokerId,
            SecurityTicker = t.Security.Ticker,
            SecurityName = t.Security.Name,
            BrokerName = t.Broker.Name,
            Date = t.Date,
            PricePerShare = t.PricePerShare,
            Quantity = t.Quantity,
            Type = t.Type
        });
    }

    public async Task<TransactionResponse?> GetByIdAsync(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        if (transaction == null) return null;

        return new TransactionResponse
        {
            Id = transaction.Id,
            SecurityId = transaction.SecurityId,
            BrokerId = transaction.BrokerId,
            SecurityTicker = transaction.Security.Ticker,
            SecurityName = transaction.Security.Name,
            BrokerName = transaction.Broker.Name,
            Date = transaction.Date,
            PricePerShare = transaction.PricePerShare,
            Quantity = transaction.Quantity,
            Type = transaction.Type
        };
    }

    public async Task<(UpdateResult Result, TransactionResponse? Data)> UpdateAsync(Guid id, UpdateTransactionRequest request)
    {
        var transaction = await _repository.GetByIdAsync(id);
        if (transaction == null) return (UpdateResult.NotFound, null);

        var security = await _securityRepository.GetByIdAsync(request.SecurityId!.Value);
        if (security == null) return (UpdateResult.InvalidReference, null);

        var broker = await _brokerRepository.GetByIdAsync(request.BrokerId!.Value);
        if (broker == null) return (UpdateResult.InvalidReference, null);

        transaction.SecurityId = request.SecurityId!.Value;
        transaction.BrokerId = request.BrokerId!.Value;
        transaction.Date = request.Date!.Value;
        transaction.PricePerShare = request.PricePerShare;
        transaction.Quantity = request.Quantity;
        transaction.Type = request.Type!.Value;

        await _repository.SaveAsync();

        return (UpdateResult.Success, new TransactionResponse
        {
            Id = transaction.Id,
            SecurityId = transaction.SecurityId,
            BrokerId = transaction.BrokerId,
            SecurityTicker = security.Ticker,
            SecurityName = security.Name,
            BrokerName = broker.Name,
            Date = transaction.Date,
            PricePerShare = transaction.PricePerShare,
            Quantity = transaction.Quantity,
            Type = transaction.Type
        });
    }

    public async Task<DeleteResult> DeleteAsync(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        if (transaction == null) return DeleteResult.NotFound;

        await _repository.DeleteAsync(transaction);
        return DeleteResult.Success;
    }
}
