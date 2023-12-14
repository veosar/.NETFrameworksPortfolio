using CQRSApi.Models.Commands;
using CQRSApi.Repositories.WriteRepositories;
using MediatR;

namespace CQRSApi.Handlers.CommandHandlers;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMovieWriteRepository _movieWriteRepository;

    public DeleteMovieCommandHandler(IMovieWriteRepository movieWriteRepository)
    {
        _movieWriteRepository = movieWriteRepository;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        await _movieWriteRepository.DeleteAsync(request.Id);
    }
}