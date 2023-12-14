using MediatR;

namespace CQRSApi.Models.Queries;

public class GetMovieByIdQuery : IRequest<Movie?>
{
    public Guid Id { get; set; }
}