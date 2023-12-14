namespace CQRSApi.Models.Requests;

public class CreateMovieRequest
{
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateOnly DateOfRelease { get; set; }
}