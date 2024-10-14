using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Games.Commands;
using GameGuessr.Api.Infrastructure.Common;
using GameGuessr.Api.Infrastructure.DepedencyInjection;
using GameGuessr.Api.Infrastructure.Hangfire;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Games.Jobs;

[Injectable]
public class SeedGamesWithYouTubeSoundtracksJob : IRecurringJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<SeedGamesWithYouTubeSoundtracksJob> _logger;

    public SeedGamesWithYouTubeSoundtracksJob(IMediator mediator, ILogger<SeedGamesWithYouTubeSoundtracksJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [DisableConcurrentExecution(5)]
    [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    [BackgroundJob]
    public async Task RunAsync()
    {
        var (_, isFailure, error) = await _mediator.Send(new SeedGamesWithYouTubeSoundtracksCommand());

        if (isFailure)
            _logger.LogWarning(error);
    }
}