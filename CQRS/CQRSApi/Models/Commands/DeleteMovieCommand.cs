using MediatR;

namespace CQRSApi.Models.Commands;

public class DeleteMovieCommand : IRequest
{
    public Guid Id { get; set; }
}