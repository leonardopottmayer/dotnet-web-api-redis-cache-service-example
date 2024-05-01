using Newtonsoft.Json;
using StackExchange.Redis;

namespace WebApi.Redis.Cache.Impl
{
    public class RedisCacheService : ICacheService
    {
        #region Attributes

        protected readonly IRedisConnectionFactory _redisConnectionFactory;

        #endregion Attributes

        #region Constructors

        public RedisCacheService(IRedisConnectionFactory redisConnectionFactory)
        {
            _redisConnectionFactory = redisConnectionFactory;
        }

        #endregion Constructors

        #region Methods

        public T? Get<T>(string key)
        {
            IDatabase conn = _redisConnectionFactory.GetDatabase();

            RedisValue value = conn.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
        {
            IDatabase conn = _redisConnectionFactory.GetDatabase();

            if (Exists(key))
            {
                return false;
            }

            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);

            string serializedValue = JsonConvert.SerializeObject(value);
            bool isSet = conn.StringSet(key, serializedValue, expiryTime);

            return isSet;
        }

        public bool Remove(string key)
        {
            IDatabase conn = _redisConnectionFactory.GetDatabase();

            if (Exists(key) == true)
            {
                return conn.KeyDelete(key);
            }

            return false;
        }

        public bool Exists(string key)
        {
            IDatabase conn = _redisConnectionFactory.GetDatabase();

            return conn.KeyExists(key);
        }

        #endregion Methods
    }
}
