namespace CS_TT_Examples;

public class OptionValidation
{
    private static Option<string> GetSomeValue() => "Some value";

    private static Option<string> GetNoValue() => null!;
    
    
    [Fact]
    public void TestSome()
    {
        var some = GetSomeValue();
        Assert.True(some is Some<string>);
        Assert.Equal("Some value", some);
    }

    [Fact]
    public void TestNone()
    {
        var none = GetNoValue();
        Assert.Null(none);
    }

    [Theory]
    [InlineData("Hello world")]
    [InlineData(null)]
    public void TestSomeWithNull(string value)
    {
        // We can use the implicit conversion from T to Option<T> to create a Some<T> value or a None<T> value
        Option<string> option = value;
        Match(option,
            v => Assert.Equal(option, v),
            () => AssertExtensions.None(option));
    }
    
    [Theory]
    [InlineData("Hello world")]
    [InlineData(null)]
    public void TestPutItInABox(string value)
    {
        // Let's but our value in a box, we can always take it out later again, or move it around.
        var option = value.ToOption();
        Match(option,
            v => Assert.Equal(option, v),
            () => AssertExtensions.None(option));
    }
}