using Hangfire.Server;

namespace GameGuessr.Api.Infrastructure.Hangfire;

public interface IRecurringJob
{
    Task RunAsync();
}
