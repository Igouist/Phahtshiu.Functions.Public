using MediatR;

namespace Phahtshiu.Functions.Application.Github;

public record GithubPushedEventCommand(
    string RepositoryName,
    string CommitMessage,
    string PusherName) : IRequest;

public class GithubPushedEventCommandHandler : IRequestHandler<GithubPushedEventCommand>
{
    public Task Handle(
        GithubPushedEventCommand request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(request.CommitMessage);
    }
}