using crossapp.repository;
using crossapp.rules;
using crossapp.services;
using domain.rules;
using domain.services._base;
using entities._base;
using Microsoft.Extensions.DependencyInjection;

namespace domain
{
    public static class ExtensionHelper
    {
        public static IServiceCollection AddService<T>(this IServiceCollection services) where T : class, IEntity
        {
            services.AddTransient<IEntityRepository<T>, canalonline.data.GenericRepository<T>>();
            services.AddTransient<IRepository<T>>(x => x.GetService<IEntityRepository<T>>());

            services.AddTransient<IRuleProcessor<T>, RuleProcessor<T>>();

            services.AddTransient<IService<T>, ServiceGeneric<T>>();

            return services;
        }
    }
}
