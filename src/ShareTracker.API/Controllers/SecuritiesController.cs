using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;

namespace ShareTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecuritiesController : ControllerBase
{
    private readonly ISecurityService _securityService;

    public SecuritiesController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSecurityRequest request)
    {
        var id = await _securityService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), new { id }, null);
    }
}