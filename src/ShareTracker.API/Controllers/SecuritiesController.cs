using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Enums;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var securities = await _securityService.GetAllAsync();
        return Ok(securities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var security = await _securityService.GetByIdAsync(id);
        if (security == null) return NotFound();
        return Ok(security);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _securityService.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success  => NoContent(),
            DeleteResult.NotFound => NotFound(),
            DeleteResult.Conflict => Conflict(),
            _ => throw new InvalidOperationException()
        };
    }
}