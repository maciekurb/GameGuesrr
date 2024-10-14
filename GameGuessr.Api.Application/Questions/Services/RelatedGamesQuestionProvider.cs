using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;
using Microsoft.EntityFrameworkCore;

namespace GameGuessr.Api.Application.Questions.Services;

public sealed class RelatedGamesQuestionProvider : GamesQuestionProviderBase
{
    public override GameMode StrategyType() => GameMode.RelatedGames;

    public RelatedGamesQuestionProvider(AppDbContext dbContext)
        : base(dbContext) { }

    public override Task<Result<List<Game>>> Execute(CancellationToken cancellationToken) =>
        Result.Success()
           .Bind(() => GetGamesForQuestion(cancellationToken))
           .Map(games =>
            {
                var game = games.First();
                var gameName = game.Name.Replace("'", "''");

                //Avoid games from the same series
                var ignoreGames = _dbContext.Games
                   .FromSqlRaw(
                        $@"select ""Id"", ""Name"" from (select ""Id"", ""Name"", DIFFERENCE(""Name"", '{gameName}') as Diff from public.""Games"") as Sim where Sim.Diff = 4")
                   .Select(x => new
                    {
                        x.Id, x.Name
                    })
                   .ToList();

                var ids = ignoreGames.Select(x => x.Id);

                var suggestedGames = _dbContext.GameSuggestions
                   .Include(x => x.Game)
                   .Include(x => x.SuggestedGame)
                   .Where(x => x.GameId == game.Id)
                   .Where(x => ids.Contains(x.SuggestedGameId) == false)
                   .Select(x => x.SuggestedGame)
                   .OrderBy(x => Guid.NewGuid())
                   .Take(3)
                   .ToList();

                var gameWithSuggestedGames = new List<Game> { game };
                gameWithSuggestedGames.AddRange(suggestedGames);

                if (gameWithSuggestedGames.Count == 4)
                    return gameWithSuggestedGames;
                
                var missingGames = games.Skip(1).Take(4 - gameWithSuggestedGames.Count).ToList();
                gameWithSuggestedGames.AddRange(missingGames);

                return gameWithSuggestedGames;
            });
}
