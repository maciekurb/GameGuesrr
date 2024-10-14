using MediatR;

namespace GameGuessr.Api.Domain.Genres;

public class NewGenresEvent : INotification
{
    public Game Game { get; }
    public string[] Names { get; }

    public NewGenresEvent(Game game, string[] names)
    {
        Game = game;
        Names = names;
    }
}
