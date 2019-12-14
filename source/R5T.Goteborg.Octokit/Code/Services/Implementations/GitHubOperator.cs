using System;
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
                    throw new Exception(EnumHelper.UnexpectedEnumerationValueMessage(repository.License));
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
    }
}
