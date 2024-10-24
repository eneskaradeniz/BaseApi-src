﻿namespace BaseApi.Infrastructure.Messaging.Settings;

public sealed class MessageBrokerSettings
{
    public const string SettingsKey = "MessageBroker";

    public string HostName { get; set; } = string.Empty;

    public int Port { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string QueueName { get; set; } = string.Empty;
}