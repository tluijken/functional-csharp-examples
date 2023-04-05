namespace CS_TT_Examples;

public class MapValidations
{
    private readonly Person _person;

    public MapValidations()
    {
        _person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 42,
            // Lets add a little recursion, shall we, just one level deep.
            Spouse = new Person
            {
                FirstName = "Jane",
                LastName = "Doe",
                Age = 42
            }
        };
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(50, 122)]
    [InlineData(25, 77)]
    [InlineData(75, 167)]
    public void TestDegreesToFahrenheitImperative(int degrees, double fahrenheit)
    {
        // Notice a, b and c live longer than they need to and can be used outside of the scope they were intended for.
        // I could potentially use a, b or c in another part of the code and get unexpected results.
        var a = degrees * 9;
        var b = a / 5;
        var c = b + 32;
        Assert.Equal(fahrenheit, c);
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(50, 122)]
    [InlineData(25, 77)]
    [InlineData(75, 167)]
    public void TestDegreesToFahrenheitFunctional(double degrees, double fahrenheit)
    {
        // Using Map, the variables a, b and c are scoped to the Map call and cannot be used outside of it.
        // I can no longer use a, b or c in another part of the code and get unexpected results.
        var c = degrees
            .Map(d => d * 9)
            .Map(a => a / 5)
            .Map(b => b + 32);
        Assert.Equal(fahrenheit, c); ;
        Assert.Equal(fahrenheit, c);
    }

    [Fact]
    public void CheckPersonSpouseImperative()
    {
        var spouse = _person.Spouse;
        // We have to check if spouse is null before we can check if spouse.FirstName is null.
        // This causes nested if statements and makes the code harder to read.
        // The focus of the code is lost in the noise of the null checks.
        if (spouse is not null)
        {
            var spouseFirstName = spouse.FirstName;
            if (spouseFirstName is not null)
            {
                if (spouseFirstName.Length > 0)
                {
                    Assert.Equal("Jane", spouseFirstName);
                }
            }
            else
            {
                throw new Exception("I told you this would happen at runtime!");
            }
        }
        else
        {
            throw new Exception("I told you this would happen at runtime!");
        }
    }

    [Fact]
    public void CheckPersonSpouseFunctional()
    {
        // Using Map, we can map the spouse to spouseFirstName and check if it is null in one step.
        // This makes the code easier to read and the focus of the code is more obvious.
        var spouseFirstName = _person
            .Map(a => a.Spouse)
            .Map(spouse => spouse.FirstName);
        Assert.Equal("Jane", spouseFirstName);
    }

    [Fact]
    public void CheckPersonSpouseFunctionalNoSpouse()
    {
        // Because we are using Map, every map results in an Option<T> which can be None or Some.
        // The next map will only be executed if the previous map returned Some.
        // This is also referred to as railway oriented programming.
        // If at some point in the chain, the result is None, the chain will just move on with None until the end of the chain.
        var spouseFirstName = _person
            .Map(a => a.Spouse)
            .Map(spouse => spouse.Spouse)
            .Map(spouse => spouse.Spouse)
            .Map(spouse => spouse.Spouse)
            .Map(spouse => spouse.Spouse)
            .Map(spouse => spouse.Spouse)
            .Map(spouse => spouse.FirstName);
        AssertExtensions.None(spouseFirstName);
    }
}
