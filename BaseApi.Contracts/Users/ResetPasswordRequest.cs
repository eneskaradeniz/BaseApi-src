namespace BaseApi.Contracts.Users;

public sealed record ResetPasswordRequest(string Email, string Password, string ConfirmPassword);