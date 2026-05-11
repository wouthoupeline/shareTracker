using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.Interfaces;

namespace ShareTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("invested")]
    public async Task<IActionResult> GetPerformance()
    {
        var result = await _analyticsService.GetPerformanceAsync();
        return Ok(result);
    }
}
