using System;

using Octokit;


namespace R5T.Goteborg.Octokit
{
    /// <summary>
    /// Default GitHub client is authenticted.
    /// Uses the <see cref="IAuthenticatedGitHubClientProvider"/> service to return an authenticated GitHub client.
    /// </summary>
    public class DefaultGitHubClientProvider : IGitHubClientProvider
    {
        private IAuthenticatedGitHubClientProvider AuthenticatedGitHubClientProvider { get; }


        public DefaultGitHubClientProvider(IAuthenticatedGitHubClientProvider authenticatedGitHubClientProvider)
        {
            this.AuthenticatedGitHubClientProvider = authenticatedGitHubClientProvider;
        }

        public GitHubClient GetGitHubClient()
        {
            var gitHubClient = this.AuthenticatedGitHubClientProvider.GetAuthenticatedGitHubClient();
            return gitHubClient;
        }
    }
}
