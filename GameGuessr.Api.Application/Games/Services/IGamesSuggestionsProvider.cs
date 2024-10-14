using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure.DepedencyInjection;

namespace GameGuessr.Api.Application.Games.Services;

[Injectable]
public interface IGamesSuggestionsProvider
{
    Task<Result<IReadOnlyList<GameSuggestion>>> ExecuteAsync(IReadOnlyList<Game> games, CancellationToken cancellationToken);
}
