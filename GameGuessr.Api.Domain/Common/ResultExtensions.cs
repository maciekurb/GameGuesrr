using CSharpFunctionalExtensions;

namespace GameGuessr.Api.Domain.Common;

public static class ResultExtensions
{
    public static async Task<Result> GetResult<T>(this Task<Result<T>> valueResult) 
        => await valueResult;

    public static Result GetResult<T>(this Result<T> valueResult) 
        => valueResult;

    public static Result EnsureNotNull<T>(this Result result, T target, string errorMessage) =>
        result.Ensure(() => target != null, errorMessage.GetWithInfo<T>());

    public static Result EnsureNotNull<T>(this Result result, T target, string errorOrigin, string errorMessage) =>
        result.Ensure(() => target != null, errorMessage.GetWithInfo(errorOrigin));

    public static Result<T> EnsureNotNull<T>(this Result<T> result, string errorMessage) =>
        result.Ensure(x => x != null, errorMessage.GetWithInfo<T>());

    public static Task<Result<T>> EnsureNotNull<T>(this Task<Result<T>> result, string errorMessage) =>
        result.Ensure(x => x != null, errorMessage.GetWithInfo<T>());

    public static Result EnsureHasValue(this Result result, string @string, string varName = "string") =>
        result.Ensure(@string.HasValue, $"Provided { varName } is null or empty");
    
    public static Result EnsureIsNotDefault(this Result result, int value, string varName = "int") =>
        result.Ensure(() => value != default, $"Provided { varName } has default value");
    
    public static Result EnsureIsNotDefault(this Result result, Guid value, string varName = "Guid") =>
        result.Ensure(() => value != default, $"Provided { varName } has default value");
}
