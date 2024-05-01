using StackExchange.Redis;

namespace WebApi.Redis.Cache.Impl
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        #region Attributes

        protected ConnectionMultiplexer Connection { get; set; }
        protected string ConnectionString { get; }

        #endregion Attributes

        #region Constructors

        public RedisConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion Constructors

        #region Methods

        public ConnectionMultiplexer GetConnection()
        {
            if (Connection is null || !Connection.IsConnected)
            {
                Connection = ConnectionMultiplexer.Connect(ConnectionString);
            }

            return Connection;
        }

        public IDatabase GetDatabase()
        {
            var connection = GetConnection();
            return connection.GetDatabase();
        }

        #endregion Methods
    }
}
