using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Services;

public class BrokerService : IBrokerService
{
    private readonly IBrokerRepository _repository;

    public BrokerService(IBrokerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(CreateBrokerRequest request)
  {
      var broker = new Broker
      {
          Id = Guid.NewGuid(),
          Name = request.Name,
      };

      await _repository.AddAsync(broker);
      return broker.Id;
  }

     public async Task<IEnumerable<BrokerResponse>> GetAllAsync()
    {
        var brokers = await _repository.GetAllAsync();
        return brokers.Select(b => new BrokerResponse
        {
            Id = b.Id,
            Name = b.Name,
        });
    }

    public async Task<BrokerResponse?> GetByIdAsync(Guid id)
    {
        var broker = await _repository.GetByIdAsync(id);
        if (broker == null) return null;

        return new BrokerResponse
        {
            Id = broker.Id,
            Name = broker.Name
        };
    }

    public async Task<(UpdateResult Result, BrokerResponse? Data)> UpdateAsync(Guid id, UpdateBrokerRequest request)
    {
        var broker = await _repository.GetByIdAsync(id);
        if (broker == null) return (UpdateResult.NotFound, null);

        broker.Name = request.Name;

        await _repository.SaveAsync();

        return (UpdateResult.Success, new BrokerResponse
        {
            Id = broker.Id,
            Name = broker.Name
        });
    }

    public async Task<DeleteResult> DeleteAsync(Guid id)
    {
        var broker = await _repository.GetByIdAsync(id);
        if (broker == null) return DeleteResult.NotFound;

        var hasPurchases = await _repository.HasPurchasesAsync(id);
        if (hasPurchases) return DeleteResult.Conflict;

        await _repository.DeleteAsync(broker);
        return DeleteResult.Success;
    }
}