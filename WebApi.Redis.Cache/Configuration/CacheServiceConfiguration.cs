using Microsoft.Extensions.DependencyInjection;
using WebApi.Redis.Cache.Impl;

namespace WebApi.Redis.Cache.Configuration
{
    public static class CacheServiceConfiguration
    {
        /// <summary>
        /// Register cache service for dependency injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheConnectionString"></param>
        public static void ConfigureCacheService(this IServiceCollection services, string cacheConnectionString)
        {
            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>(c => { return new RedisConnectionFactory(cacheConnectionString); });

            services.AddSingleton<ICacheService, RedisCacheService>(c => {
                var redisConnectionFactory = c.GetService<IRedisConnectionFactory>();
                return new RedisCacheService(redisConnectionFactory);
            });
        }
    }
}
