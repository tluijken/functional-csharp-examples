namespace CS_TT_Examples;

// Not a functional example, but it's a good example of how to use an indexer.
// It also cleans up our code a lot.
public class IndexerValidations
{
    private readonly MovieData _movieData;
    
    public IndexerValidations()
    {
        _movieData = new MovieData(new[]
        {
            new Movie("The Shawshank Redemption", "Frank Darabont", 1994),
            new Movie("The Godfather", "Francis Ford Coppola", 1972),
            new Movie("The Godfather: Part II", "Francis Ford Coppola", 1974),
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Good, the Bad and the Ugly", "Sergio Leone", 1966),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Pulp Fiction", "Quentin Tarantino", 1994),
            new Movie("Schindler's List", "Steven Spielberg", 1993),
            new Movie("2001 A Space Odyssey", "Stanley Kubrick", 1968),
            new Movie("The Lord of the Rings: The Fellowship of the Ring", "Peter Jackson", 2001),
            new Movie("Fight Club", "David Fincher", 1999),
        });
    }

    [Fact]
    public void GetMoviesByPublicParameter()
    {
        // Lets get the movies with an index of 3, 5 and 7 at a way I frequently see in the wild.
        // Although this isn't that bad, there is room for improvement.
        var movieList = new[]
        {
            _movieData.Movies[3],
            _movieData.Movies[5],
            _movieData.Movies[7]
        };
        
        Assert.Equal(new[]
        {
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Schindler's List", "Steven Spielberg", 1993)
        }, movieList);
    }
    
    [Fact]
    public void GetMoviesByIndexer()
    {
        // What if we could get the movies by using an indexer directly on the MovieData instance?
        var movieList = new[]
        {
            _movieData[3],
            _movieData[5],
            _movieData[7]
        };
        Assert.Equal(new[]
        {
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Schindler's List", "Steven Spielberg", 1993)
        }, movieList);
    }


    [Fact]
    public void GetMoviesByIndexerRange()
    {
        // Or better yet, what if we could pass in a range of indexes?
        var movieList = _movieData[3, 5, 7];
        Assert.Equal(new[]
        {
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Schindler's List", "Steven Spielberg", 1993)
        }, movieList);
    }
    
    [Fact]
    public void GetMoviesByPublicProperty()
    { 
        // Lets get wild and get the movies using a predicate.
        var movieList = _movieData[m => m.Year > 1990];
        Assert.Equal(new []
        {
            new Movie("The Shawshank Redemption", "Frank Darabont", 1994),
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Pulp Fiction", "Quentin Tarantino", 1994),
            new Movie("Schindler's List", "Steven Spielberg", 1993),
            new Movie("The Lord of the Rings: The Fellowship of the Ring", "Peter Jackson", 2001),
            new Movie("Fight Club", "David Fincher", 1999),
        }, movieList);
    }

    [Fact]
    public void GetMoviesByTitle()
    {
        // Or just get the movies by title.
        var movieList = new[]
        {
            _movieData["The Dark Knight"],
            _movieData["The Lord of the Rings: The Return of the King"],
            _movieData["Schindler's List"]
        };
        Assert.Equal(new[]
        {
            new Movie("The Dark Knight", "Christopher Nolan", 2008),
            new Movie("The Lord of the Rings: The Return of the King", "Peter Jackson", 2003),
            new Movie("Schindler's List", "Steven Spielberg", 1993)
        }, movieList);
    }
}