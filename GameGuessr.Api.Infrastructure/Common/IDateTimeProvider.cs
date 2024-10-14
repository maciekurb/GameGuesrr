using GameGuessr.Api.Infrastructure.DepedencyInjection;

namespace GameGuessr.Api.Infrastructure.Common;

[Injectable]
public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
