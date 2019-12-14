using System;

using Octokit;


namespace R5T.Goteborg.Octokit
{
    /// <summary>
    /// Default unauthenticated GitHub client provider uses the <see cref="IProductHeaderValueProvider"/> service to just provide an unauthenticated GitHub client.
    /// </summary>
    public class DefaultUnauthenticatedGitHubClientProvider : IUnauthenticatedGitHubClientProvider
    {
        private IProductHeaderValueProvider ProductHeaderValueProvider { get; }


        public DefaultUnauthenticatedGitHubClientProvider(IProductHeaderValueProvider productHeaderValueProvider)
        {
            this.ProductHeaderValueProvider = productHeaderValueProvider;
        }

        public GitHubClient GetUnauthenticatedGitHubClient()
        {
            var productHeaderValue = this.ProductHeaderValueProvider.GetProductHeaderValue();

            var gitHubClient = new GitHubClient(productHeaderValue);
            return gitHubClient;
        }
    }
}
