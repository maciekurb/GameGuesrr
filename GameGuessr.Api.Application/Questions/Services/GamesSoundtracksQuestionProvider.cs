using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Games.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace GameGuessr.Api.Application.Questions.Services;

public class GamesSoundtracksQuestionProvider : GamesQuestionProviderBase
{
    private readonly IEnumerable<IYouTubeOstProvider> _youTubeOstProviders;

    public GamesSoundtracksQuestionProvider(AppDbContext dbContext, IEnumerable<IYouTubeOstProvider> youTubeOstProviders)
        : base(dbContext)
    {
        _youTubeOstProviders = youTubeOstProviders;
    }

    public override GameMode StrategyType() => GameMode.Soundtrack;

    public override Task<Result<List<Game>>> Execute(CancellationToken cancellationToken) =>
        Result.Success()
           .Bind(() => GetGamesForQuestion(cancellationToken, true, true))
           .Ensure(games => games.Count == 4, games => $"Missing {4 - games.Count} games")
           .Map(games =>
            {
                _youTubeOstProviders.GetStrategyOf(YouTubeOstProviderType.Raw).ExecuteAsync(games[0], cancellationToken);
                return games;
            });
}
