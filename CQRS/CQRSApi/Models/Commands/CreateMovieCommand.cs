using MediatR;

namespace CQRSApi.Models.Commands;

public class CreateMovieCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateOnly DateOfRelease { get; set; }
}