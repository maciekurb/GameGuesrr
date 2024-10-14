using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Common.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure.DepedencyInjection;

namespace GameGuessr.Api.Application.Games.Services;

[Injectable(AllowManyImplementations = true)]
public interface IYouTubeOstProvider : IStrategy<YouTubeOstProviderType>
{
    Task<Result<Game>> ExecuteAsync(Game game, CancellationToken cancellationToken);
}