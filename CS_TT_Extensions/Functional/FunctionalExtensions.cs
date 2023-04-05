namespace CS_TT_Extensions.Functional;

public static class FunctionalExtensions
{
    public static void Match<T>(Option<T> option, Action<T> some, Action none)
    {
        switch (option)
        {
            case Some<T> someOption:
                some(someOption.Value);
                break;
            default:
                none();
                break;
        }
    }

    // Notice that 'All' will also return early as soon as one of the validations fails
    public static bool Validate<T>(this T @this, params Func<T, bool>[] predicates) => predicates.All(p => p(@this));

    // The term 'Tee' is named after the Unix command 'tee', which is named after the T-shaped pipe fitting.
    public static Option<T> Tee<T>(this Option<T> @this, Action<Option<T>> action)
    {
        action(@this);
        return @this;
    }

    // We might want to have a Tee method for non-option types as well.
    public static T Tee<T>(this T @this, Action<T> action)
    {
        action(@this);
        return @this;
    }
}
