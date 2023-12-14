using CQRSApi.Models;
using CQRSApi.Models.Queries;
using CQRSApi.Repositories.ReadRepositories;
using MediatR;

namespace CQRSApi.Handlers;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie?>
{
    private readonly IMovieReadRepository _movieReadRepository;

    public GetMovieByIdQueryHandler(IMovieReadRepository movieReadRepository)
    {
        _movieReadRepository = movieReadRepository;
    }
    public async Task<Movie?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _movieReadRepository.GetByIdAsync(request.Id);
        return result;
    }
}