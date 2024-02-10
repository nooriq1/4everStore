using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _4everStore.Models;

namespace _4everStore.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {



        }
        public DbSet<Catgory> catgories { get; set; }
        public DbSet<Iteam> Iteams { get; set; }
        //public DbSet<Cart> carts { get; set; }
        public DbSet<request> requests { get; set; }
        



    }
}
