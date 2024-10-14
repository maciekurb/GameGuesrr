using Hangfire;
namespace GameGuessr.Api.Infrastructure.Hangfire;

[AttributeUsage(AttributeTargets.Method)]
public class BackgroundJobAttribute : Attribute
{
    public string CronExpression { get; }
    public bool Enabled { get; }

    public BackgroundJobAttribute(bool enabled = true)
    {
        CronExpression = Cron.Never();
        Enabled = enabled;
    }
    
    public BackgroundJobAttribute(string cronExpression, bool enabled = true)
    {
        CronExpression = cronExpression;
        Enabled = enabled;
    }
}
