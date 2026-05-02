using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Entities;

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
}