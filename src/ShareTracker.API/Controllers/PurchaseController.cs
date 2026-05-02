using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;

namespace ShareTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _brokerService;

    public PurchasesController(IPurchaseService brokerService)
    {
        _brokerService = brokerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePurchaseRequest request)
    {
        var id = await _brokerService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), new { id }, null);
    }
}