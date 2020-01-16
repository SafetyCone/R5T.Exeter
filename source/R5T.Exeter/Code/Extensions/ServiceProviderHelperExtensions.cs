using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Dacia;
using R5T.Dacia.Extensions;


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
    }
}
