using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;

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
}