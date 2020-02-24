using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Langobard;


namespace R5T.Exeter
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The standard way of adding and <see cref="IConfiguration"/> instance, as a singleton.
        /// </summary>
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            return services;
        }

        public static IServiceCollection AddBasicLogging(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddBasicLogging();
            });

            return services;
        }
    }
}
