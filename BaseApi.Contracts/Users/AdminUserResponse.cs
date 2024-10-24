namespace BaseApi.Contracts.Users;

public sealed class AdminUserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public bool EmailVerified { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberVerified { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }
}