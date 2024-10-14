using GameGuessr.Api.Domain.Genres;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Genres.Events;

public class NewGenresEventHandler : INotificationHandler<NewGenresEvent>
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<NewGenresEventHandler> _logger;

    public NewGenresEventHandler(AppDbContext dbContext, ILogger<NewGenresEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(NewGenresEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Game.Id == default)
            throw new ArgumentException(nameof(notification.Game.Id));

        foreach (var name in notification.Names)
        {
            var genreResult = GameGenre.Create(notification.Game.Id, name);

            if (genreResult.IsFailure)
                throw new EventException(genreResult.Error);

            var genre = genreResult.Value;

            await _dbContext.GameGenres.AddAsync(genre, cancellationToken);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Genres saved in database");
    }
}