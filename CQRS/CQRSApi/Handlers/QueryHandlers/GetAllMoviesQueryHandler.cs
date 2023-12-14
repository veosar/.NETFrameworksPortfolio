using CQRSApi.Models;
using CQRSApi.Models.Queries;
using CQRSApi.Repositories.ReadRepositories;
using MediatR;

namespace CQRSApi.Handlers;

public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<Movie>>
{
    private readonly IMovieReadRepository _movieReadRepository;

    public GetAllMoviesQueryHandler(IMovieReadRepository movieReadRepository)
    {
        _movieReadRepository = movieReadRepository;
    }

    public async Task<List<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var result = await _movieReadRepository.GetAllAsync();
        return result;
    }
}