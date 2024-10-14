using MediatR;

namespace GameGuessr.Api.Domain.Platforms;

public class NewPlatformsEvent : INotification
{
    public Game Game { get; }
    public string[] Names { get; }

    public NewPlatformsEvent(Game game, string[] names)
    {
        Game = game;
        Names = names;
    }
}