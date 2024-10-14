namespace GameGuessr.Api.Infrastructure.Hangfire;

public class JobConfigDetails
{
    public string TypeName { get; set; }
    public string Id { get; set; }
    public bool Enabled { get; set; }
    public string Cron { get; set; }
    public Type Type { get; set; }
}
