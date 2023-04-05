namespace CS_TT_Examples.Models;

public record Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Age { get; init; }
    public Person Spouse { get; init; }
}