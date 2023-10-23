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

    // GET https://localhost:800/movie
    [HttpGet]

    public IEnumerable<Movie> GetMovies() 
    {
        var movies = context.Movie;

        return movies;
    }
}
