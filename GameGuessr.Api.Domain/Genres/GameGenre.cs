using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;

namespace GameGuessr.Api.Domain.Genres;

public class GameGenre : IAuditable
{
    internal GameGenre() { }

    [Key]
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public Game Game { get; internal set; }
    public Guid GameId { get; internal set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<GameGenre> Create(Guid gameId, string name) =>
        Result.Success()
           .EnsureIsNotDefault(gameId, nameof(gameId))
           .EnsureHasValue(name, nameof(name))
           .Map(() => new GameGenre
            {
                GameId = gameId,
                Name = name
            });
}
