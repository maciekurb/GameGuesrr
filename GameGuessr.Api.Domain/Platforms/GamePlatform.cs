using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;

namespace GameGuessr.Api.Domain.Platforms;

public class GamePlatform : IAuditable
{
    internal GamePlatform() { }

    [Key]
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public Game Game { get; internal set; }
    public Guid GameId { get; internal set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<GamePlatform> Create(Guid gameId, string name) =>
        Result.Success()
           .EnsureIsNotDefault(gameId, nameof(gameId))
           .EnsureHasValue(name, nameof(name))
           .Map(() => new GamePlatform
            {
                GameId = gameId,
                Name = name
            });
}
