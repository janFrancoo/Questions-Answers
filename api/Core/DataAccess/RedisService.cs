using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly int _redisPort;

        private ConnectionMultiplexer _redis;

        public RedisService(IConfiguration config)
        {
            _redisHost = config["Redis:Host"];
            _redisPort = Convert.ToInt32(config["Redis:Port"]);
        }

        public void Connect()
        {
            try
            {
                var configStr = $"{_redisHost}:{_redisPort},connectRetry=5";
                _redis = ConnectionMultiplexer.Connect(configStr);
            } 
            catch (RedisConnectionException exception)
            {
                throw exception;
            }
        }

        public async Task Set(string key, string value)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value, TimeSpan.FromDays(1));
        }

        public async Task<string> Get(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }
    }
}
