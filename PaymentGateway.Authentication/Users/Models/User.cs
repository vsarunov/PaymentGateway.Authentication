using Microsoft.AspNetCore.Identity;

namespace PaymentGateway.Authentication.Users.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
