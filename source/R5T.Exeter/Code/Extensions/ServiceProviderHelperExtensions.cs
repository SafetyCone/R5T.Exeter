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

        public static T GetInstance<T>(this ServiceProviderHelper serviceProviderHelper, IConfiguration configuration, Action<ILoggingBuilder> configureLoggingAction, Action<IServiceCollection> configureServicesAction)
            where T: class
        {
            var startupServiceProvider = serviceProviderHelper.GetServiceProvider(
                configuration,
                configureLoggingAction,
                configureServicesAction);

            var instance = startupServiceProvider.GetRequiredService<T>();
            return instance;
        }

        /// <summary>
        /// Gets an instance using an empty configuration and logging provided by <see cref="LoggingBuilderHelper.AddDefaultLogging(ILoggingBuilder)"/>.
        /// </summary>
        public static T GetInstanceWithEmptyConfigurationAndDefaultLogging<T>(this ServiceProviderHelper serviceProviderHelper, Action<IServiceCollection> configureServicesAction)
            where T: class
        {
            var emptyConfiguration = ConfigurationHelper.GetEmptyConfiguration();

            var instance = serviceProviderHelper.GetInstance<T>(
                emptyConfiguration,
                LoggingBuilderHelper.AddDefaultLogging,
                configureServicesAction);

            return instance;
        }

        /// <summary>
        /// Uses the <see cref="GetInstanceWithEmptyConfigurationAndDefaultLogging{T}(ServiceProviderHelper, Action{IServiceCollection})"/> method.
        /// Note: adds the <typeparamref name="T"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static T GetInstance<T>(this ServiceProviderHelper serviceProviderHelper)
            where T: class
        {
            var instance = serviceProviderHelper.GetInstanceWithEmptyConfigurationAndDefaultLogging<T>(services =>
            {
                services.AddSingleton<T>();
            });
            return instance;
        }
    }
}
