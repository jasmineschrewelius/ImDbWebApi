using ImDbWebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImDbWebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
      { }

    public DbSet<Movie> Movie { get; set; }  

}