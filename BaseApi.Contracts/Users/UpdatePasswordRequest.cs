namespace BaseApi.Contracts.Users;

public sealed record UpdatePasswordRequest(
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword);