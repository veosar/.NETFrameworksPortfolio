using CQRSApi.Models;
using CQRSApi.Models.Commands;
using CQRSApi.Repositories.WriteRepositories;
using MediatR;

namespace CQRSApi.Handlers.CommandHandlers;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Guid>
{
    private readonly IMovieWriteRepository _movieWriteRepository;

    public CreateMovieCommandHandler(IMovieWriteRepository movieWriteRepository)
    {
        _movieWriteRepository = movieWriteRepository;
    }

    public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var movie = new Movie
        {
            Id = guid,
            Genre = request.Genre,
            Title = request.Title,
            DateOfRelease = request.DateOfRelease
        };
        await _movieWriteRepository.CreateAsync(movie);

        return guid;
    }
}