using System;

using Microsoft.Extensions.Options;

using Octokit;

using R5T.Polidea;


namespace R5T.Goteborg.Octokit
{
    /// <summary>
    /// Uses the <see cref="IUnauthenticatedGitHubClientProvider"/> service to get a GitHub client, and adds credentials from a <see cref="GitHubAuthentication"/> options instance.
    /// </summary>
    public class DefaultAuthenticatedGitHubClientProvider : IAuthenticatedGitHubClientProvider
    {
        private IUnauthenticatedGitHubClientProvider UnauthenticatedGitHubClientProvider { get; }
        private IOptions<GitHubAuthentication> GitHubAuthentication { get; }


        public DefaultAuthenticatedGitHubClientProvider(IUnauthenticatedGitHubClientProvider unauthenticatedGitHubClientProvider, IOptions<GitHubAuthentication> gitHubAuthentication)
        {
            this.UnauthenticatedGitHubClientProvider = unauthenticatedGitHubClientProvider;
            this.GitHubAuthentication = gitHubAuthentication;
        }

        public GitHubClient GetAuthenticatedGitHubClient()
        {
            var gitHubAuthentication = this.GitHubAuthentication.Value;

            var gitHubClient = this.UnauthenticatedGitHubClientProvider.GetUnauthenticatedGitHubClient();

            var basicAuthentication = new Credentials(gitHubAuthentication.UserName, gitHubAuthentication.Password);

            gitHubClient.Credentials = basicAuthentication;

            return gitHubClient;
        }
    }
}
