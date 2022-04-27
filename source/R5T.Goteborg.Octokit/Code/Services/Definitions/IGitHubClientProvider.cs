using System;

using Octokit;using R5T.T0064;


namespace R5T.Goteborg.Octokit
{[ServiceDefinitionMarker]
    public interface IGitHubClientProvider:IServiceDefinition
    {
        GitHubClient GetGitHubClient();
    }
}
