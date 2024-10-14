using CSharpFunctionalExtensions;
using GameGuessr.Api.Infrastructure.Common;

namespace GameGuessr.Api.Infrastructure.Hangfire;

public abstract class HangfireJobBase
{
    protected static Func<Result, Result> HandleCommandResult() =>
        result => result.IsFailure ? throw new BackgroundJobException(result.Error) : result;
}
