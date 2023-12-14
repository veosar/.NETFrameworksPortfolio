using CQRSApi.Models;
using CQRSApi.Models.Commands;
using CQRSApi.Repositories.WriteRepositories;
using MediatR;

namespace CQRSApi.Handlers.CommandHandlers;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IMovieWriteRepository _movieWriteRepository;

    public UpdateMovieCommandHandler(IMovieWriteRepository movieWriteRepository)
    {
        _movieWriteRepository = movieWriteRepository;
    }

    public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Id = request.Id,
            Genre = request.Genre,
            Title = request.Title,
            DateOfRelease = request.DateOfRelease
        };
        
        await _movieWriteRepository.UpdateAsync(movie);
    }
}