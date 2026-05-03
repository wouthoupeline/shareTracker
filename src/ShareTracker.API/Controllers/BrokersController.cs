using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Enums;

namespace ShareTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrokersController : ControllerBase
{
    private readonly IBrokerService _brokerService;

    public BrokersController(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrokerRequest request)
    {
        var id = await _brokerService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brokers = await _brokerService.GetAllAsync();
        return Ok(brokers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var broker = await _brokerService.GetByIdAsync(id);
        if (broker == null) return NotFound();
        return Ok(broker);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _brokerService.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success  => NoContent(),
            DeleteResult.NotFound => NotFound(),
            DeleteResult.Conflict => Conflict(),
            _ => throw new InvalidOperationException()
        };
    }
}