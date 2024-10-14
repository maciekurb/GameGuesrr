namespace GameGuessr.Api.Domain.Common;

public static class ErrorExtensions
{
    public static string GetWithInfo<T>(this string error) => $"{typeof(T).Name} : {error}";
    public static string GetWithInfo(this string error, string origin) => $"{origin} : {error}";
}
