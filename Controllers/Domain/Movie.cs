using System.ComponentModel.DataAnnotations;

namespace ImDbWebApi.Domain;

public class Movie
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Plot { get; set; }

    [MaxLength(50)]
    public string Genre { get; set; }

    [MaxLength(50)]
    public string Director { get; set; }

    public int ReleaseYear { get; set; }
    
}