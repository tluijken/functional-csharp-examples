using Xunit.Sdk;

namespace CS_TT_Examples.Helpers;

internal sealed class AssertExtensions : Assert
{
    public static void None<T>(Option<T> option)
    {
        switch (option)
        {
            case Some<T>:
                throw new TrueException("Expected None, but got Some", false);
            case None<T>:
                return;
        }
    }
}