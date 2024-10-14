using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Games.Commands;
using GameGuessr.Api.Infrastructure.Common;
using GameGuessr.Api.Infrastructure.DepedencyInjection;
using GameGuessr.Api.Infrastructure.Hangfire;
using Hangfire;
using MediatR;

namespace GameGuessr.Api.Application.Games.Jobs;

[Injectable]
public class SeedGamesSuggestionsJob : IRecurringJob
{
    private readonly IMediator _mediator;

    public SeedGamesSuggestionsJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    [DisableConcurrentExecution(5)]
    [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    [BackgroundJob]
    public async Task RunAsync()
    {
        var (_, isFailure, error) = await _mediator.Send(new SeedGamesSuggestionsCommand());

        if (isFailure)
            throw new BackgroundJobException(error);
    }
}