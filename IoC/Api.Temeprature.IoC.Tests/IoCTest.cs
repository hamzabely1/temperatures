using Api.Temperature.Data.Context.Contract;
using Api.Temperature.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Temeprature.IoC.Application;

namespace Api.Temeprature.IoC.Tests
{
    public static class IoCTest
    {
        /// <summary>
        /// Configuration de l'injection des repository du Web API RestFul
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureInjectionDependencyRepositoryTest(this IServiceCollection services)
        {
            services.ConfigureInjectionDependencyRepository();

            return services;

        }


        /// <summary>
        /// Configure l'injection des services
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInjectionDependencyServiceTest(this IServiceCollection services)
        {
            services.ConfigureInjectionDependencyService();

            return services;
        }


        /// <summary>
        /// Configuration de la connexion de la base de données en mémoire pour l'environnement de test
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureDBContextTest(this IServiceCollection services)
        {
            services.AddDbContext<IMeteoFranceDBContext,MeteoFranceDBContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                );

            return services;
        }
    }
}
