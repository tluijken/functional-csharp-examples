using CS_TT_Extensions.Functional;

namespace CS_TT_Extensions;

public static class MapExtensions
{
    //public static TTarget Map<TSource, TTarget>(this TSource source, Func<TSource, TTarget> factory) => factory(source);

    public static Option<TTarget> Map<TSource, TTarget>(this TSource source, Func<TSource, TTarget> factory) =>
        source.ToOption().Map(factory);

    public static Option<TTarget> Map<TSource, TTarget>(this Option<TSource> source, Func<TSource, TTarget> factory) =>
        source switch
        {
            Some<TSource> some => TryCreate(() => factory(some.Value)),
            _ => new None<TTarget>()
        };

    public static IEnumerable<Option<TTarget>> Map<TSource, TTarget>(this IEnumerable<TSource> sources, Func<TSource, TTarget> factory) => sources.Select(s => s.Map(factory));

    #region private helper functiona
    private static Option<T> TryCreate<T>(Func<T> func)
    {
        try
        {
            return new Some<T>(func());
        }
        catch
        {
            return new None<T>();
        }
    }

    #endregion

}
