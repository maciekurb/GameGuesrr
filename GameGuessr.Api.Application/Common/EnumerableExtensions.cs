using GameGuessr.Api.Application.Common.Services;

namespace GameGuessr.Api.Application.Common;

public static class EnumerableExtensions
{
    public static TStrategy GetStrategyOf<TStrategy, TStrategyResolver>(this IEnumerable<TStrategy> strategies, TStrategyResolver strategyResolver)
        where TStrategy : IStrategy<TStrategyResolver>
        where TStrategyResolver : struct 
            => strategies.Single(str => str.Is(strategyResolver));
}
