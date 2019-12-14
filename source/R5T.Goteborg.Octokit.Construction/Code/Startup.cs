using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Coventry;
using R5T.Polidea.Sardinia;
using R5T.Polidea.Suebia;

using R5T.Goteborg.Octokit.Default;
using R5T.Goteborg.Octokit.Sardinia;


namespace R5T.Goteborg.Octokit.Construction
{
    public class Startup : ApplicationStartupBase
    {
        public Startup(ILogger<Startup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServicesProvider)
        {
            base.ConfigureConfigurationBody(configurationBuilder, configurationServicesProvider);

            configurationBuilder
                .AddGitHubAuthenticationConfiguration(configurationServicesProvider)
                ;
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            base.ConfigureServicesBody(services);

            services
                .AddGitHubAuthentication()
                .AddGitHubConfiguration()
                .AddDefaultOctokitGitHubOperatorServices<ConfigurationBasedProductHeaderValueProvider>()
                ;
        }
    }
}
