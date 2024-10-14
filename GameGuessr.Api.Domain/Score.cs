using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Shared;

namespace GameGuessr.Api.Domain;

public class Score : IAuditable
{
    internal Score() { }

    [Key]
    public Guid Id { get; internal set; }
    public Player Player { get; internal set; }
    public Guid PlayerId { get; internal set; }
    public int Points { get; internal set; }
    public GameMode Mode { get; internal set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static Result<Score> Create(int points, GameMode mode, Guid playerId) =>
        Result.Success()
           .EnsureIsNotDefault(playerId, nameof(playerId))
           .Map(() => new Score
            {
                PlayerId = playerId,
                Points = points,
                Mode = mode
            });
}


