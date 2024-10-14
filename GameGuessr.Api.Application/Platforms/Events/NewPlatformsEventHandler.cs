using GameGuessr.Api.Domain.Platforms;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Platforms.Events;

public class NewPlatformsEventHandler : INotificationHandler<NewPlatformsEvent>
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<NewPlatformsEventHandler> _logger;

    public NewPlatformsEventHandler(AppDbContext dbContext, ILogger<NewPlatformsEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(NewPlatformsEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Game.Id == default)
            throw new ArgumentException(nameof(notification.Game.Id));

        foreach (var name in notification.Names)
        {
            var platformResult = GamePlatform.Create(notification.Game.Id, name);

            if (platformResult.IsFailure)
                throw new EventException(platformResult.Error);

            var platform = platformResult.Value;

            await _dbContext.GamePlatforms.AddAsync(platform, cancellationToken);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Platforms saved in database");
    }
}