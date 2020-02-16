using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PaymentGateway.Authentication.Users.Models;

namespace PaymentGateway.Authentication.Users.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<Either<Error, bool>>
        {
            public Command(string firstName, string lastName, string password, string username, string email)
            {
                FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
                LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
                Username = username ?? throw new ArgumentNullException(nameof(username));
                Password = password ?? throw new ArgumentNullException(nameof(password));
                Email = email ?? throw new ArgumentNullException(nameof(email));
            }

            public string Username { get; }
            public string Email { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string Password { get; }
        }

        public class Handler : IRequestHandler<Command, Either<Error, bool>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Either<Error, bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User { UserName = request.Username, FirstName = request.FirstName, LastName = request.LastName, Email = request.Email };

                var result = await _userManager.CreateAsync(user, request.Password);

                string role = "Basic User";

                if (result.Succeeded)
                {
                    if (_roleManager.FindByNameAsync(role).Result == null)
                    {
                        _roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                    await _userManager.AddToRoleAsync(user, role);
                    await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
                    await _userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
                    await _userManager.AddClaimAsync(user, new Claim("lastName", user.LastName));
                    await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
                    await _userManager.AddClaimAsync(user, new Claim("role", role));
                    return true;
                }

                return Error.New(string.Join(".", result.Errors.Select(x => x.Description)));
            }
        }
    }
}
