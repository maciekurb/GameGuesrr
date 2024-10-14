using GameGuessr.Api.Application.Services;

namespace GameGuessr.Api.Extensions;

public static class HangfireExtensions
{
    public static void UseHanfireService(this WebApplication webApp, ILogger<HangfireService> logger)
    {
        var hangfireService = new HangfireService(logger);
        hangfireService.ScheduleJobs();
    }
}
