namespace BaseApi.Infrastructure.Infrastructure;

public sealed class AzureBlobConnectionString(string value)
{
    public const string SettingsKey = "AzureBlob";

    public string Value { get; } = value;

    public static implicit operator string(AzureBlobConnectionString azureBlobConnectionString) => azureBlobConnectionString.Value;
}
