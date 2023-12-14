namespace CQRSApi.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateOnly DateOfRelease { get; set; }
    
}