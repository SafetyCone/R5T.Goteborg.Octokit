using System;
using System.Linq;
using System.Threading.Tasks;

using Octokit;

using R5T.Magyar;


namespace R5T.Goteborg.Octokit
{
    public class GitHubOperator : IGitHubOperator
    {
        private IGitHubClientProvider GitHubClientProvider { get; }


        public GitHubOperator(IGitHubClientProvider gitHubClientProvider)
        {
            this.GitHubClientProvider = gitHubClientProvider;
        }

        public async Task<long> CreateRepository(GitHubRepository repository)
        {
            var gitHubClient = this.GitHubClientProvider.GetGitHubClient();

            var autoInit = repository.InitializeWithReadMe;

            string licenseTemplate;
            switch(repository.License)
            {
                case GitHubRepositoryLicense.MIT:
                    licenseTemplate = LicenseHelper.MitLicenseIdentifier;
                    break;

                default:
                    throw new Exception(EnumerationHelper.UnexpectedEnumerationValueMessage(repository.License));
            }

            var @private = repository.Visibility == GitHubRepositoryVisibility.Private ? true : false; // Private if private, otherwise public.

            var newRepository = new NewRepository(repository.Name)
            {
                AutoInit = true, // Create with README for immediate checkout.
                Description = repository.Description,
                LicenseTemplate = licenseTemplate,
                Private = @private, // Default is public (private = false).
            };

            var createdRepository = await gitHubClient.Repository.Create(newRepository);

            return createdRepository.Id;
        }

        public async Task<bool> RepositoryExists(string owner, string name)
        {
            var gitHubClient = this.GitHubClientProvider.GetGitHubClient();

            var searchRequest = new SearchRepositoriesRequest(name)
            {
                User = owner,
            };

            var searchResult = await gitHubClient.Search.SearchRepo(searchRequest);

            var exists = searchResult.Items.Where(x => x.Name == name).Count() > 0;
            return exists;
        }

        public async Task DeleteRepository(string owner, string name)
        {
            var gitHubClient = this.GitHubClientProvider.GetGitHubClient();

            await gitHubClient.Repository.Delete(owner, name);
        }

        public async Task<string> GetRepositoryCheckoutUrl(string owner, string name)
        {
            var gitHubClient = this.GitHubClientProvider.GetGitHubClient();

            var repository = await gitHubClient.Repository.Get(owner, name);

            var checkoutURL = repository.CloneUrl;
            return checkoutURL;
        }

        public async Task<string> GetRepositoryCheckoutUrl(long repositoryID)
        {
            var gitHubClient = this.GitHubClientProvider.GetGitHubClient();

            var repository = await gitHubClient.Repository.Get(repositoryID);

            var checkoutURL = repository.CloneUrl;
            return checkoutURL;
        }
    }
}
