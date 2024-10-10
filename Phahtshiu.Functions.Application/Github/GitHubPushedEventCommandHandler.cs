using MediatR;

namespace Phahtshiu.Functions.Application.Github;

public record GitHubPushedEventCommand(
    string RepositoryName,
    string CommitMessage,
    string PusherName) : IRequest;

public class GitHubPushedEventCommandHandler : IRequestHandler<GitHubPushedEventCommand>
{
    public Task Handle(
        GitHubPushedEventCommand request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(request.CommitMessage);
    }
}