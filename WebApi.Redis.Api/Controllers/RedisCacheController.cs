using Microsoft.AspNetCore.Mvc;
using WebApi.Redis.Cache;
using WebApi.Redis.Example.Models;

namespace WebApi.Redis.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisCacheController : ControllerBase
    {
        private readonly ILogger<RedisCacheController> _logger;
        private readonly ICacheService _cacheService;

        public RedisCacheController(ILogger<RedisCacheController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        [HttpPost(Name = "CacheData")]
        public IActionResult CacheData([FromBody] CacheDataDto data)
        {
            bool savedObject = _cacheService.Set(data.Key, data, DateTimeOffset.Now.AddSeconds(Convert.ToDouble(data.ExpirationTimeInSeconds)));
            bool savedString = _cacheService.Set(data.Key + "-string", data.StringValue, DateTimeOffset.Now.AddSeconds(Convert.ToDouble(data.ExpirationTimeInSeconds)));
            bool savedNumber = _cacheService.Set(data.Key + "-number", data.NumberValue, DateTimeOffset.Now.AddSeconds(Convert.ToDouble(data.ExpirationTimeInSeconds)));
            bool savedBool = _cacheService.Set(data.Key + "-bool", data.BoolValue, DateTimeOffset.Now.AddSeconds(Convert.ToDouble(data.ExpirationTimeInSeconds)));

            return Ok(new { savedObject, savedString, savedNumber, savedBool });
        }

        [HttpGet("GetCachedData/{key}", Name = "GetCachedData")]
        public IActionResult GetCachedData([FromRoute] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Invalid key.");
            }

            CacheDataDto cachedObject = _cacheService.Get<CacheDataDto>(key);
            string cachedString = _cacheService.Get<string>(key + "-string");
            long cachedNumber = _cacheService.Get<long>(key + "-number");
            bool cachedBool = _cacheService.Get<bool>(key + "-bool");

            return Ok(new { cachedObject, cachedString, cachedNumber, cachedBool });
        }

        [HttpGet("KeyExists/{key}", Name = "KeyExists")]
        public IActionResult KeyExists([FromRoute] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Invalid key.");
            }

            bool dataExists = _cacheService.Exists(key);
            bool stringExists = _cacheService.Exists(key + "-string");
            bool numberExists = _cacheService.Exists(key + "-number");
            bool boolExists = _cacheService.Exists(key + "-bool");

            return Ok(new { dataExists, stringExists, numberExists, boolExists });
        }

        [HttpDelete("RemoveKey/{key}", Name = "RemoveKey")]
        public IActionResult RemoveKey([FromRoute] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Invalid key.");
            }

            bool removedData = _cacheService.Remove(key);
            bool removedString = _cacheService.Remove(key + "-string");
            bool removedNumber = _cacheService.Remove(key + "-number");
            bool removedBool = _cacheService.Remove(key + "-bool");

            return Ok(new { removedData, removedString, removedNumber, removedBool });
        }
    }
}
