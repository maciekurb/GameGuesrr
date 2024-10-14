using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure.DepedencyInjection;

namespace GameGuessr.Api.Application.Games.Services
{
    [Injectable]
    public interface IGamesProvider
    {
        public Task<Result<IReadOnlyList<Game>>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
