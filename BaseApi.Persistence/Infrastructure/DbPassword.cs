namespace BaseApi.Persistence.Infrastructure;

public sealed class DbPassword(string value)
{
    public const string SettingsKey = "DB_PASSWORD";

    public string Value { get; } = value;
    
    public static implicit operator string(DbPassword dbPassword) => dbPassword.Value;
}