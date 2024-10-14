using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure.DepedencyInjection;
using GameGuessr.Shared;

namespace GameGuessr.Api.Application.Questions.Services;

[Injectable(AllowManyImplementations = true)]
public interface IGamesQuestionProvider : IStrategy<GameMode>
{
    Task<Result<List<Game>>> Execute(CancellationToken cancellationToken);
}
