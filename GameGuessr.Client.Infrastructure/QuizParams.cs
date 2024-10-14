using GameGuessr.Shared;

namespace GameGuessr.Client.Infrastructure;

public class QuizParams
{
    public GameMode Mode { get; set; }
    public int Score { get; set; }
}
