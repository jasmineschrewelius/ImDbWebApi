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

    // GET https://localhost:8000/movie/{id}, exempelvis https://localhost:8000/movie/1
    [HttpGet("{id}")]

    public ActionResult<Movie> GetMovie(int id)  // search for movie with id number
    {
        var movie = context.Movie.FirstOrDefault(x => x.Id == id);

        if (movie is null)
        {
            return NotFound();  // send back error message, 404 Not Found to client
        }
        
        return movie; // will send message, 200 OK

    }
}
