using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;
using Microsoft.EntityFrameworkCore;

namespace GameGuessr.Api.Application.Questions.Services;

public abstract class GamesQuestionProviderBase : Strategy<GameMode>, IGamesQuestionProvider
{
    public abstract Task<Result<List<Game>>> Execute(CancellationToken cancellationToken);
    
    protected readonly AppDbContext _dbContext;

    protected GamesQuestionProviderBase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected Task<Result<List<Game>>> GetGamesForQuestion(CancellationToken cancellationToken, bool onlyTopGames = false, bool onlyGamesWithOSt = false)
        => Result.Success()
           .Map(async () =>
            {
                var gamesQuery = _dbContext.Games
                   .Include(x => x.Platforms)
                   .Include(x => x.Genres)
                   .Include(x => x.Screenshots)
                   .Where(x => x.Screenshots.Any())
                   .Where(x => x.MetacriticScore >= 85);

                if (onlyTopGames)
                    gamesQuery = gamesQuery.Where(x => x.MetacriticScore >= 85);
                
                if (onlyGamesWithOSt)
                    gamesQuery = gamesQuery.Where(x => x.OstYouTubeKey != null);

                return await gamesQuery.OrderBy(x => Guid.NewGuid())
                   .Take(4)
                   .ToListAsync(cancellationToken);
            });
}
