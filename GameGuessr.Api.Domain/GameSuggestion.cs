using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;

namespace GameGuessr.Api.Domain;

public class GameSuggestion : IAuditable
{
    internal GameSuggestion() { }

    [Key]
    public Guid Id { get; internal set; }
    public Game Game { get; internal set; }
    public Guid GameId { get; internal set; }
    
    public Game SuggestedGame { get; internal set; }
    public Guid SuggestedGameId { get; internal set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<GameSuggestion> Create(Guid gameId, Guid suggestedGameId) =>
        Result.Success()
           .EnsureIsNotDefault(gameId, nameof(gameId))
           .EnsureIsNotDefault(suggestedGameId, nameof(suggestedGameId))
           .Map(() => new GameSuggestion
            {
                GameId = gameId,
                SuggestedGameId = suggestedGameId
            });
}
