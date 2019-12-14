using System;

using Octokit;


namespace R5T.Goteborg.Octokit
{
    public interface IAuthenticatedGitHubClientProvider
    {
        GitHubClient GetAuthenticatedGitHubClient();
    }
}
