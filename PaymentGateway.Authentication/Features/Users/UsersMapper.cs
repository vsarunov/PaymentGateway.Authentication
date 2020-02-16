using PaymentGateway.Authentication.Users.Commands;

namespace PaymentGateway.Authentication.Features.Users
{
    internal static class UsersMapper
    {
        internal static CreateUser.Command ToCommand(this UserDto user) =>
            new CreateUser.Command(user.FirstName, user.LastName, user.Password, user.Username, user.Email);
    }
}
