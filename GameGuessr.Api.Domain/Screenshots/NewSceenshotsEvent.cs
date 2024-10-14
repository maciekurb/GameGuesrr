using MediatR;

namespace GameGuessr.Api.Domain.Screenshots;

public class NewSceenshotsEvent : INotification
{
    public Game Game { get; }
    public string[] Urls { get; }

    public NewSceenshotsEvent(Game game, string[] urls)
    {
        Game = game;
        Urls = urls;
    }
}
