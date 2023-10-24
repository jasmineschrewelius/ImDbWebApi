using System.ComponentModel.DataAnnotations;
using ImDbWebApi.Data;
using ImDbWebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ImDbWebApi.Controllers;

[Route("[controller]")]
[ApiController]

public class MovieController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public MovieController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // GET https://localhost:8000/movie   METOD
    [HttpGet]

    public IEnumerable<Movie> GetMovies([FromQuery] string? title)  // GET ALL MOVIES OR MOVIE BY TITLE
    {
        IEnumerable<Movie> movies;

        if (title is not null)  // if the search invole a specific title, ex. Alien, only show movies to that title
        {
            movies = context.Movie.Where(x => x.Title.Contains(title));
        }
        else // if the search dont invole title , show all movies
        {
            movies = context.Movie.ToList();
        }
        
        return movies;
    }

    // GET https://localhost:8000/movie/{id}, ex https://localhost:8000/movie/1
    [HttpGet("{id}")]

    public ActionResult<Movie> GetMovie(int id)  // Get a movie with id number
    {
        var movie = context.Movie.FirstOrDefault(x => x.Id == id);

        if (movie is null)
        {
            return NotFound();  // send back error message, 404 Not Found to client
        }
        
        return movie; // will send message, 200 OK

    }

 // Get in data that looks like this
 //   {
 //     "title": "Aliens",
 //     "plot": "Omboard a space ship with aliens",
 //     "genre": "Action, Sci-fi",
 //     "director": "Ridley Scott",
 //     "releaseYear": 1986
//    }
    [HttpPost]
    public ActionResult<MovieDto> CreateMovie(CreateMovieRequest createMovieRequest) // Create a movie
    {
        var movie = MapToMovie(createMovieRequest);

        context.Movie.Add(movie);

        context.SaveChanges();

        var movieDto = MapToMovieDto(movie);  // we use DTO beacuse if we want change the entity, it does not change the api

        return Created("", movieDto); // 201 Created
    }

    // Delete / movie/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteMovie(int id) // delete movie based on the id
    {
        var movie = context.Movie.FirstOrDefault(x => x.Id == id); // search if the movie id input is the same as a movie id in the collection of movies in database

        if (movie is null) // if we dont find a movie with same id
        {
            return NotFound(); // return status code 404 Not Found
        }

        context.Movie.Remove(movie); // if the same movie id is found, remove from collection

        context.SaveChanges(); // save the changes

        return NoContent(); // return status code 204 No Content
    }
    private Movie MapToMovie(CreateMovieRequest createMovieRequest) // help metod to map from a request to a movie
       => new ()
       {
          Title = createMovieRequest.Title,
          Plot = createMovieRequest.Plot,
          Genre = createMovieRequest.Genre,
          Director = createMovieRequest.Director,
          ReleaseYear = createMovieRequest.ReleaseYear
       };

    private MovieDto MapToMovieDto(Movie movie)  // help metod to map from a movie to a movieDto
       => new ()
       {
          Id = movie.Id,
          Title = movie.Title,
          Plot = movie.Plot,
          Genre = movie.Genre,
          Director = movie.Director,
          ReleaseYear = movie.ReleaseYear
       };


}

public class CreateMovieRequest   // create DTO, data transfer object, it carries data between processes
{
    [MaxLength(50)]
    [Required]
    public string Title { get; set; }

    [MaxLength(500)]
    [Required]
    public string Plot { get; set; }

    [MaxLength(50)]
    [Required]
    public string Genre { get; set; }

    [MaxLength(50)]
    [Required]
    public string Director { get; set; }
    
    [Required]
    public int ReleaseYear { get; set; }
}

public class MovieDto   // create DTO, data transfer object
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Plot { get; set; }

    public string Genre { get; set; }

    public string Director { get; set; }

    public int ReleaseYear { get; set; }
}
