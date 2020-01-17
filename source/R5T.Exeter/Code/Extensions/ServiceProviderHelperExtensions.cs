using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Chamavia;
using R5T.Dacia;
using R5T.Dacia.Extensions;
using R5T.Langobard;


namespace R5T.Exeter
{
    public static class ServiceProviderHelperExtensions
    {
        public static IServiceProvider GetServiceProvider(this ServiceProviderHelper serviceProviderHelper, IConfiguration configuration, Action<ILoggingBuilder> configureLoggingAction, Action<IServiceCollection> configureServicesAction)
        {
            var serviceProvider = new ServiceCollection()
                .AddConfiguration(configuration)
                .AddLogging(configureLoggingAction)
                .AddServices(configureServicesAction)

                .BuildServiceProvider();

            return serviceProvider;
        }

        public static T GetInstance<T>(this ServiceProviderHelper serviceProviderHelper)
            where T: class
        {
            var emptyConfiguration = ConfigurationHelper.GetEmptyConfiguration();

            var startupServiceProvider = ServiceProviderHelper.New()
                .GetServiceProvider(emptyConfiguration, LoggingBuilderHelper.AddDefaultLogging,
                    (services) =>
                    {
                        services.AddSingleton<T>();
                    });

            var applicationStartup = startupServiceProvider.GetRequiredService<T>();
            return applicationStartup;
        }
    }
}
