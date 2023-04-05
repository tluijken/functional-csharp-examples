namespace CS_TT_Examples.Models;

public record Movie(string Title, string Director, int Year);

public record MovieData(Movie[] Movies)
{
    // We can create an indexer by using the record's positional parameters.
    public Movie? this[int index] => Movies.ElementAtOrDefault(index);

    // We can create an indexer by using the record's positional parameters.
    public IEnumerable<Movie?> this[params int[] indices] => indices.Select(i => this[i]);

    // We can create an indexer by using the record's properties.
    public Movie? this[string title] => Movies.FirstOrDefault(m => m.Title == title);

    // We can create an indexer by using a lambda expression.
    public IEnumerable<Movie?> this[Func<Movie, bool> predicate] => Movies.Where(predicate);
}
