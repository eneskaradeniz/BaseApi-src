namespace BaseApi.Persistence.Infrastructure;

public sealed class ConnectionString(string value)
{
    public const string SettingsKey = "Database";

    public string Value { get; } = value;

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}