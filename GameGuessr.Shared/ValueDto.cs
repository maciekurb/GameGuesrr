namespace GameGuessr.Shared;

public class ValueDto<T>
{
    public T Value { get; set; }

    public static ValueDto<T> Create(T value) => new() { Value = value };

    public static implicit operator T(ValueDto<T> dto) =>
        dto == null
            ? default
            : dto.Value;
}
