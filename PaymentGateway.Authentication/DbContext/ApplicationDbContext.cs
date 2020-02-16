using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Authentication.Users.Models;

namespace PaymentGateway.Authentication.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
   

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
