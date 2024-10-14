using GameGuessr.Shared;

namespace GameGuessr.Client.Infrastructure;

public class Ranking
{
    public List<RankingItem> RankingItems { get; set; }
    public GameMode Mode { get; set; }
}
