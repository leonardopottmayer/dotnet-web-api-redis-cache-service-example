using StackExchange.Redis;

namespace WebApi.Redis.Cache
{
    public interface IRedisConnectionFactory
    {
        /// <summary>
        /// Get Redis Connection
        /// </summary>
        /// <returns></returns>
        ConnectionMultiplexer GetConnection();

        /// <summary>
        /// Get Redis Database
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();
    }
}
