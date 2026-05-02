using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;

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
        return brokers.Select(s => new BrokerResponse
        {
            Name = s.Name,
        });
    }

    public async Task<BrokerResponse?> GetByIdAsync(Guid id)
    {
        var broker = await _repository.GetByIdAsync(id);
        if (broker == null) return null;

        return new BrokerResponse
        {
            Name = broker.Name
        };
    }
}