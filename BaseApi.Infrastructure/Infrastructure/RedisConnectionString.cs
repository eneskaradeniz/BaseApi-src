namespace BaseApi.Infrastructure.Infrastructure;

public sealed class RedisConnectionString(string value)
{
    public const string SettingsKey = "Redis";
    
    public string Value { get; } = value;
    
    public static implicit operator string(RedisConnectionString redisConnectionString) => redisConnectionString.Value;
}