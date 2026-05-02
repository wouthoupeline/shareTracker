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
}