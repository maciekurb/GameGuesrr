using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;

namespace GameGuessr.Api.Application.Questions.Services;

public class AllGamesQuestionProvider : GamesQuestionProviderBase
{
    public override GameMode StrategyType() => GameMode.All;

    public AllGamesQuestionProvider(AppDbContext dbContext)
        : base(dbContext) { }

    public override Task<Result<List<Game>>> Execute(CancellationToken cancellationToken) =>
        Result.Success()
           .Bind(() => GetGamesForQuestion(cancellationToken));
}
