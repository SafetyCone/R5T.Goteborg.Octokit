using System;

using Octokit;


namespace R5T.Goteborg.Octokit
{
    public interface IGitHubClientProvider
    {
        GitHubClient GetGitHubClient();
    }
}
