using MediatR;

namespace CQRSApi.Models.Commands;

public class UpdateMovieCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateOnly DateOfRelease { get; set; }
}