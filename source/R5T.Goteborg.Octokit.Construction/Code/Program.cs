using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Coventry;


namespace R5T.Goteborg.Octokit.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.TestGitHubOperatorCreateRepository();
            //Program.TestGitHubOperatorDeleteRepository();
        }

        private static void TestGitHubOperatorDeleteRepository()
        {
            var serviceProvider = Program.GetServiceProvider();

            var gitHubOperator = serviceProvider.GetRequiredService<IGitHubOperator>();

            // Delete a repository.
        }

        private static void TestGitHubOperatorCreateRepository()
        {
            var serviceProvider = Program.GetServiceProvider();

            var gitHubOperator = serviceProvider.GetRequiredService<IGitHubOperator>();

            // Create a repository.
            var repository = GitHubRepository.GetDefault(Constants.TestingRepositoryName, Constants.TestingRepositoryDescription);

            var id = gitHubOperator.CreateRepository(repository).Result;

            Console.WriteLine($"Created repository '{repository.Name}' with ID: {id}");
        }

        private static IServiceProvider GetServiceProvider()
        {
            var serviceProvider = ApplicationBuilder.New().UseStartupFromCoventryWithDerbyConfigurationStartup<Startup>();
            return serviceProvider;
        }
    }
}
