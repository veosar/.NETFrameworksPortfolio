namespace CQRSApi.Models.Requests;

public class UpdateMovieRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateOnly DateOfRelease { get; set; }
}