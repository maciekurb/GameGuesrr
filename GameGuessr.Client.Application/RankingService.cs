using GameGuessr.Shared;

namespace GameGuessr.Client.Application;

public class RankingService
{
    public event Action<GameMode> RefreshRequested;

    public void CallRequestRefresh(GameMode mode)
    {
        RefreshRequested?.Invoke(mode);
    }
}
