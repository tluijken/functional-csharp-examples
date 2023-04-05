namespace CS_TT_Examples;

public class CurryingValidation
{
    private static readonly Func<double, Func<double, double>> Multiply = x => y => y * x;
    private static readonly Func<double, Func<double, double>> Divide = x => y => y / x;
    private static readonly Func<double, Func<double, double>> Add = x => y => y + x;
    private static readonly Func<double, Func<double, double>> Subtract = x => y => y - x;
    private static readonly Func<double, Func<double, double>> Modulo = x => y => y % x;
    private static readonly Func<double, Func<double, double>> Power = x => y => Math.Pow(y, x);

    // This is the same as the Multiply function, but written in a more verbose way.
    private static Func<double, double> MultiplyAlt(double x)
    {
        // Notice that a curried function works inside out, so the inner function is the one that is returned.
        // The first parameter is actually the multiplier, which is the second parameter of the Multiply function.
        // It returns a function that takes the value to multiply with the multiplier set in the outer function.
        return (double y) =>
        {
            return y * x;
        };
    }
    
    [Fact]
    public void TestMultiply()
    {
        // This is how we would normally call a curried function
        var multiply = Multiply(2)(2);
        Assert.Equal(4, multiply);
    }
    
    [Fact]
    public void TestMultiplyAlt()
    {
        // This is how we would normally call a curried function, notice is has the same result als the shorthand version
        var multiply = MultiplyAlt(2)(2);
        Assert.Equal(4, multiply);
    }
    
    [Fact]
    public void TestReusingMultiply()
    {
        // To demonstrate partial application, we can reuse the Multiply function by setting de multiplier to 2
        // and then call it with different values to multiple with 2.
        var multiplyByTwo = Multiply(2);
        Assert.Equal(4, multiplyByTwo(2));
        Assert.Equal(6, multiplyByTwo(3));
        Assert.Equal(8, multiplyByTwo(4));
    }
    
    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(50, 122)]
    [InlineData(25, 77)]
    [InlineData(75, 167)]
    public void TestCelsiusToFahrenheitCurried(double degrees, double fahrenheit)
    {
        // There we go, look at that, we can now write our function in a more functional way using currying
        var f = degrees
            .Map(Multiply(9))
            .Map(Divide(5))
            .Map(Add(32));
        Assert.Equal(fahrenheit, f);
    }
    
    [Theory]             
    [InlineData(0, 32)]
    [InlineData(100, 212)]                                                                                     
    [InlineData(50, 122)]                                                                                      
    [InlineData(25, 77)]                                                                                       
    [InlineData(75, 167)]                                                                                      
    public void TestCelsiusToFahrenheitCurriedLonghand(double degrees, double fahrenheit)                                 
    {                                                                                                          
        // This is the same as the previous test, but with the curried functions written out in full
        var f = degrees                                                                                  
            .Map(x => Multiply(9)(x))                                                                                  
            .Map(x => Divide(5)(x))                                                                                    
            .Map(x => Add(32)(x));                                                                                     
        Assert.Equal(fahrenheit, f);                                                                           
    }                                                                                                          
}