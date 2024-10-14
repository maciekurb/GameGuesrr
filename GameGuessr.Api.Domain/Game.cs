using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Domain.Genres;
using GameGuessr.Api.Domain.Platforms;
using GameGuessr.Api.Domain.Screenshots;
using MediatR;

namespace GameGuessr.Api.Domain;

public class Game : DomainEventsBase, IAuditable 
{
    internal Game() { }

    [Key]
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public DateTime Released { get; internal set; }
    public int MetacriticScore { get; internal set; }
    public int ExternalId { get; internal set; }

    public string OstYouTubeKey { get; internal set; }
    public int OstYouTubeDuration { get; internal set; }

    public ICollection<GameSuggestion> GameSuggestions { get; internal set; } = new HashSet<GameSuggestion>();
    public ICollection<Screenshot> Screenshots { get; internal set; } = new HashSet<Screenshot>();
    public ICollection<GameGenre> Genres { get; internal set; } = new HashSet<GameGenre>();
    public ICollection<GamePlatform> Platforms { get; internal set; } = new HashSet<GamePlatform>();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<Game> Create(string name, DateTime released, int metacriticScore, int externalId) =>
        Result.Success()
           .EnsureHasValue(name, nameof(name))
           .Map(() => new Game
            {
                Name = name,
                Released = released.SetKindUtc(),
                MetacriticScore = metacriticScore,
                ExternalId = externalId
            });

    public Result SetOstYouTubeKey(string key, int duration) =>
        Result.Success()
           .EnsureHasValue(key, nameof(key))
           .Tap(() =>
            {
                OstYouTubeKey = key;
                OstYouTubeDuration = duration;
            });

    public Result BindScreenshots(string[] screenshots) =>
        Result.Success()
           .Ensure(() => screenshots is { Length: > 0 }, "Screenshots are empty")
           .Tap(() => RaiseDomainEvent(new NewSceenshotsEvent(this, screenshots)));

    public Result BindPlatforms(string[] platforms) =>
        Result.Success()
           .TapIf(platforms is { Length: > 0 }, () => RaiseDomainEvent(new NewPlatformsEvent(this, platforms)));
    
    public Result BindGenres(string[] genres) =>
        Result.Success()
           .TapIf(genres is { Length: > 0 }, () => RaiseDomainEvent(new NewGenresEvent(this, genres)));
}
