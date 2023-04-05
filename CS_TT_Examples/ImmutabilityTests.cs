namespace CS_TT_Examples;

public class ImmutabilityTests
{
    private readonly Person _person;

    public ImmutabilityTests()
    {
        // Create a new instance of the Person class.
        // All properties are readonly, so we can't change the value of the properties.
        _person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 42,
        };
    }

    [Fact]
    public void TestImmutablePerson()
    {
        // We can't change the value of the FirstName property, because it is readonly.
        // _person.FirstName = "Jane";
        // We can't change the value of the LastName property, because it is readonly.
        // _person.LastName = "Doe";
        // We can't change the value of the Age property, because it is readonly.
        // _person.Age = 42;
        // We can't change the value of the Spouse property, because it is readonly.
        // _person.Spouse = new Person
        // {
        //     FirstName = "Jane",
        //     LastName = "Doe",
        //     Age = 42
        // };
        Assert.Equal("John", _person.FirstName);
    }

    [Fact]
    public void TestImmutablePersonWithWith()
    {
        // In functional programming, data is immutable, but we can create a new instance of the data with the changes we want.
        // Because state is not shared, concurrency is easier to manage.
        // We no longer have to worry about one thread changing the state _person value while another thread is using it.
        // Since the introduction of records in C# 9, we can use the with keyword to create a new instance of the data with the changes we want.
        // This cleared the path even further for functional programming in C#.
        // In Rust, everything is immutable by default.
        // I would encourage you to treat data as immutable by default in C# as well.
        // If the compiler complains about a variable not being able to update, you should think about whether you really need to update it.
        var alteredPerson = _person.Map(p => p with { FirstName = "Jane" }).Unwrap();
        Assert.Equal("John", _person.FirstName);
        Assert.Equal("Jane", alteredPerson.FirstName);
    }
}
