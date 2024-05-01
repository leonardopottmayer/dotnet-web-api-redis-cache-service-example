namespace WebApi.Redis.Example.Models
{
    public class CacheDataDto
    {
        public string Key { get; set; }
        public string StringValue { get; set; }
        public long NumberValue { get; set; }
        public bool BoolValue { get; set; }
        public long? ExpirationTimeInSeconds { get; set; }

        public CacheDataDto() { }
        public CacheDataDto(string key, string stringValue, long numberValue, bool boolValue, long expirationTime)
        {
            Key = key;
            StringValue = stringValue;
            NumberValue = numberValue;
            BoolValue = boolValue;
            ExpirationTimeInSeconds = expirationTime;
        }
    }
}
