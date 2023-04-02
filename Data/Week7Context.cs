using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Week7Context : IdentityDbContext<User>
    {
        public Week7Context(DbContextOptions<Week7Context> options) : base(options)
        {}
        
    }
}