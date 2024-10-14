using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Questions.Services;
using GameGuessr.Shared;
using MediatR;

namespace GameGuessr.Api.Application.Questions.Queries;

public class GetSingleQuestionQuery : IRequest<Result<GamesQuestion>>
{
    public GameMode Mode { get; set; }

    public GetSingleQuestionQuery(GameMode mode)
    {
        Mode = mode;
    }
}

public class GetSingleQuestionQueryHandler : IRequestHandler<GetSingleQuestionQuery, Result<GamesQuestion>>
{
    private readonly IEnumerable<IGamesQuestionProvider> _gamesQuestionProviders;

    public GetSingleQuestionQueryHandler(IEnumerable<IGamesQuestionProvider> gamesQuestionProviders)
    {
        _gamesQuestionProviders = gamesQuestionProviders;
    }

    public Task<Result<GamesQuestion>> Handle(GetSingleQuestionQuery request, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(() => _gamesQuestionProviders.GetStrategyOf(request.Mode))
           .Bind(strategy => strategy.Execute(cancellationToken))
           .Ensure(games => games.Count == 4, games => $"Missing {4 - games.Count} games")
           .Ensure(games => games.First().Screenshots.Any(), $"Missing screenshots")
           .Map(games =>
            {
                var game = games.First();
                var gamesNames = games.Select(x => x.Name).ToArray();
                var urls = game.Screenshots.Select(scr => scr.Url).ToArray();

                var gameQuestionBuilder = GamesQuestion.Create(game.Name, gamesNames, urls);

                if (request.Mode == GameMode.Soundtrack)
                    gameQuestionBuilder.WithYouTubeOst(game.OstYouTubeKey, game.OstYouTubeDuration);
                
                return gameQuestionBuilder.Build();
            });
}
