using System.Runtime.CompilerServices;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Games.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameGuessr.Api.Application.Games.Commands;

public class SeedGamesWithYouTubeSoundtracksCommand : IRequest<Result>  { }

public class SeedGamesWithYouTubeSoundtracksCommandHandler : IRequestHandler<SeedGamesWithYouTubeSoundtracksCommand, Result>
{
    private readonly AppDbContext _dbContext;
    private readonly IEnumerable<IYouTubeOstProvider> _youTubeOstProviders;

    public SeedGamesWithYouTubeSoundtracksCommandHandler(AppDbContext dbContext, IEnumerable<IYouTubeOstProvider> youTubeOstProviders)
    {
        _dbContext = dbContext;
        _youTubeOstProviders = youTubeOstProviders;
    }

    public Task<Result> Handle(SeedGamesWithYouTubeSoundtracksCommand request, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(async () =>
            {
                var games = await _dbContext.Games
                   .Where(g => g.MetacriticScore >= 85 && String.IsNullOrEmpty(g.OstYouTubeKey))
                   .ToListAsync(cancellationToken);

                var results = new List<Result>();

                await foreach (var resultGame in GetGamesOstAsync(games, cancellationToken))
                    results.Add(resultGame.IsSuccess ? AddYouTubeOst(resultGame.Value, cancellationToken) : resultGame);

                return results;
            })
           .Bind(results => Result.Combine(results));
    
    private async IAsyncEnumerable<Result<Game>> GetGamesOstAsync(IEnumerable<Game> games, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var game in games)
        {
            yield return await _youTubeOstProviders.GetStrategyOf(YouTubeOstProviderType.Raw).ExecuteAsync(game, cancellationToken);
            await Task.Delay(1000, cancellationToken);
        }
    }

    private Result AddYouTubeOst(Game game, CancellationToken cancellationToken) =>
        Result.Success()
           .EnsureIsNotDefault(game.Id, nameof(game.Id))
           .Tap(() =>
            {
                _dbContext.Games.Update(game);
                _dbContext.SaveChangesAsync(cancellationToken);
            });
}