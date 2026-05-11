using ShareTracker.Application.DTOs;

namespace ShareTracker.Application.Interfaces;

public interface IAnalyticsService
{
    Task<IEnumerable<SecurityPerformance>> GetPerformanceAsync();
}
