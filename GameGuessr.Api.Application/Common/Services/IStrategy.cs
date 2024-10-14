namespace GameGuessr.Api.Application.Common.Services;

public interface IStrategy<TStrategyResolver> where TStrategyResolver : struct
{
    TStrategyResolver StrategyType();
    bool Is(TStrategyResolver strategyResolver);
}
