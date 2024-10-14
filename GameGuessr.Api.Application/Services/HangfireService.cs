using System.Reflection;
using GameGuessr.Api.Application.Games.Jobs;
using GameGuessr.Api.Infrastructure.Hangfire;
using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Services;

public class HangfireService
{
    private readonly RecurringJobManager _jobManager;
    private readonly IEnumerable<JobConfigDetails> _jobs;
    private readonly ILogger<HangfireService> _logger;

    public HangfireService(ILogger<HangfireService> logger)
    {
        _logger = logger;
        _jobManager = new RecurringJobManager();
        _jobs = GetRecurringJobs();
    }

    public void ScheduleJobs()
    {
        _logger.LogInformation("Hangfire - schedule jobs");

        StopJobs();

        ScheduleJob<SeedGamesJob>();
        ScheduleJob<SeedGamesWithYouTubeSoundtracksJob>();
        ScheduleJob<SeedGamesSuggestionsJob>();
    }

    private void StopJobs()
    {
        using (var conn = JobStorage.Current.GetConnection())
        {
            foreach (var job in conn.GetRecurringJobs())
            {
                _jobManager.RemoveIfExists(job.Id);
                _logger.LogInformation($"Job has been stopped: {job.Id}");
            }
        }
    }

    private IEnumerable<JobConfigDetails> GetRecurringJobs()
    {
        var jobConfigDetails = new List<JobConfigDetails>();

        var recurringJobsTypes = Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(type => type.IsAssignableTo(typeof(IRecurringJob)) && type.IsClass);

        foreach (var jobType in recurringJobsTypes)
        {
            var recurringJobConfig = jobType
               .GetMethod(nameof(IRecurringJob.RunAsync))
              ?.GetCustomAttribute<BackgroundJobAttribute>();

            if (recurringJobConfig == null)
                throw new NullReferenceException($"Recurring job{jobType.Name} must have {nameof(BackgroundJobAttribute)} attribute defined.");

            jobConfigDetails.Add(new JobConfigDetails
            {
                Id = jobType.Name,
                Enabled = recurringJobConfig.Enabled,
                Cron = recurringJobConfig.CronExpression,
                TypeName = jobType.FullName,
                Type = jobType
            });
        }

        return jobConfigDetails;
    }

    private void ScheduleJob<T>()
        where T : IRecurringJob
    {
        var jobConfiguration = _jobs.Single(cfg => cfg.TypeName == typeof(T).FullName);

        ScheduleJob<T>(jobConfiguration);
    }

    private void ScheduleJob<T>(JobConfigDetails job)
        where T : IRecurringJob
    {
        _logger.LogInformation($"Starting job: {job.Id}");

        if (String.IsNullOrWhiteSpace(job.Cron) || String.IsNullOrWhiteSpace(job.TypeName))
            return;

        try
        {
            _jobManager.RemoveIfExists(job.Id);
            var jobType = Type.GetType(job.TypeName);

            if (jobType != null && job.Enabled)
            {
                RecurringJob.AddOrUpdate<T>(x => x.RunAsync(), job.Cron, TimeZoneInfo.Local);
                
                _logger.LogInformation($"Job {job.Id} has been added to hangfire.");
            }
            else
                _logger.LogInformation($"Job '{job.Id}' of type'{job.TypeName}' is not found or job is disabled.(Enabled:{job.Enabled})");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception has been thrown when starting the job {ex.Message}");
        }
    }
}
