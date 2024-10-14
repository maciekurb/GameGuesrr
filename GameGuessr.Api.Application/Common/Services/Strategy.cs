namespace GameGuessr.Api.Application.Common.Services;

public abstract class Strategy<TStrategyResolver> : IStrategy<TStrategyResolver> where TStrategyResolver : struct
{
    public abstract TStrategyResolver StrategyType();
    public bool Is(TStrategyResolver strategyResolver) => strategyResolver.Equals(StrategyType());
}
