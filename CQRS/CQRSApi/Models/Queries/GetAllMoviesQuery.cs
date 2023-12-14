using MediatR;

namespace CQRSApi.Models.Queries;

public class GetAllMoviesQuery : IRequest<List<Movie>>
{
    
}