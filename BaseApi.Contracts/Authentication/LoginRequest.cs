namespace BaseApi.Contracts.Authentication;

public sealed record LoginRequest(string EmailOrPhoneNumber, string Password);