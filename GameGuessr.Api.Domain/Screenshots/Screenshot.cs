using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;

namespace GameGuessr.Api.Domain.Screenshots;

public class Screenshot : IAuditable
{
    internal Screenshot() { }

    [Key]
    public Guid Id { get; internal set; }
    public Game Game { get; internal set; }
    public Guid GameId { get; internal set; }
    public string Url { get; internal set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static Result<Screenshot> Create(string url, Guid gameId) =>
        Result.Success()
           .EnsureIsNotDefault(gameId, nameof(gameId))
           .EnsureHasValue(url, nameof(url))
           .Map(() => new Screenshot
            {
                Url = url,
                GameId = gameId
            });
}
