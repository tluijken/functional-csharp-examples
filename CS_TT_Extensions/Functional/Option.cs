namespace CS_TT_Extensions.Functional;

public record Some<T>(T Value) : Option<T>
{
    // by using the implicit operator, we can directly convert a value of T to an Some<T>
    public static implicit operator Some<T>(T value) => new(value);
    
    // Additionally, we can also convert an Some<T> to a T implicitly
    public static implicit operator T(Some<T> @this) => @this.Value;
}

// Hello darkness, my old friend
public record None<T> : Option<T>;

public abstract record Option<T>
{
    // by using the implicit operator, we can directly convert a value of T to an Option<T> being either Some<T> or None<T>
    public static implicit operator Option<T>(T value) => value is null ? new None<T>() : new Some<T>(value);
}

public static class OptionExtensions
{
    // Notice that implicit conversion from T to Option<T> is used here
    public static Option<T> ToOption<T>(this T @this) => @this;
    
    public static T Unwrap<T>(this Option<T> @this) => @this switch
    {
        Some<T> some => some.Value,
        None<T> _ => throw new Exception("There was no value to unwrap")
    };
    
    public static T UnwrapOr<T>(this Option<T> @this, T defaultValue) => @this switch
    {
        Some<T> some => some.Value,
        None<T> _ => defaultValue
    };
}