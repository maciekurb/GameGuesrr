using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Games.Services;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Games.Commands;

public class SeedGamesSuggestionsCommand  : IRequest<Result> { }

public class SeedGamesSuggestionsCommandHandler : IRequestHandler<SeedGamesSuggestionsCommand, Result>
{
    private readonly AppDbContext _dbContext;
    private readonly IGamesSuggestionsProvider _gamesSuggestionsProvider;
    private readonly ILogger<SeedGamesSuggestionsCommandHandler> _logger;

    public SeedGamesSuggestionsCommandHandler(AppDbContext dbContext,
                                              IGamesSuggestionsProvider gamesSuggestionsProvider,
                                              ILogger<SeedGamesSuggestionsCommandHandler> logger)
    {
        _dbContext = dbContext;
        _gamesSuggestionsProvider = gamesSuggestionsProvider;
        _logger = logger;
    }

    public Task<Result> Handle(SeedGamesSuggestionsCommand request, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(() =>
            {
                var gamesWithoutSuggested = _dbContext.Games
                   .Include(x => x.GameSuggestions)
                   .Where(game => game.GameSuggestions.Any() == false)
                   .ToList();

                return gamesWithoutSuggested;

            })
           .Bind(games => _gamesSuggestionsProvider.ExecuteAsync(games, cancellationToken))
           .Tap(async gameSuggestions =>
            {
                await _dbContext.GameSuggestions.AddRangeAsync(gameSuggestions, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation($"All games suggestions saved in database");
            })
           .GetResult();
}
