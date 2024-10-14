using GameGuessr.Api.Domain.Screenshots;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Screenshots.Events;

public class NewSceenshotsEventHandler : INotificationHandler<NewSceenshotsEvent>
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<NewSceenshotsEventHandler> _logger;

    public NewSceenshotsEventHandler(AppDbContext dbContext, ILogger<NewSceenshotsEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(NewSceenshotsEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Game.Id == default)
            throw new ArgumentException(nameof(notification.Game.Id));

        foreach (var url in notification.Urls)
        {
            var screenShotResult = Screenshot.Create(url, notification.Game.Id);

            if (screenShotResult.IsFailure)
                throw new EventException(screenShotResult.Error);

            var screenShot = screenShotResult.Value;

            await _dbContext.Screenshots.AddAsync(screenShot, cancellationToken);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Screenshots saved in database");
    }
}
