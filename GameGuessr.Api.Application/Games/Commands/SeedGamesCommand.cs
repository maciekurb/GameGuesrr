using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Games.Services;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Games.Commands;

public class SeedGamesCommand : IRequest<Result> { }

public class SeedGamesCommandHandler : IRequestHandler<SeedGamesCommand, Result>
{
    private readonly AppDbContext _dbContext;
    private readonly IGamesProvider _gamesProvider;
    private readonly ILogger<SeedGamesCommandHandler> _logger;

    public SeedGamesCommandHandler(AppDbContext dbContext,
                                   IGamesProvider gamesProvider,
                                   ILogger<SeedGamesCommandHandler> logger)
    {
        _dbContext = dbContext;
        _gamesProvider = gamesProvider;
        _logger = logger;
    }

    public Task<Result> Handle(SeedGamesCommand request, CancellationToken cancellationToken) =>
        Result.Success()
           .Bind(() => _gamesProvider.ExecuteAsync(cancellationToken))
           .Tap(async games =>
            {
                await _dbContext.Games.AddRangeAsync(games, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Games saved in database");
            })
           .GetResult();
}
