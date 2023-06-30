using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
namespace webapi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Roles> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table requirements
            modelBuilder.Entity<User>(eb =>
            {
                eb.Property(u => u.Name).IsRequired();
                eb.Property(u => u.Password).IsRequired();
                eb.Property(u => u.Password).HasColumnType("varchar(200)");
                eb.Property(u => u.Email).IsRequired();


                //References

                eb.HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(u => u.UserId);

            });
            modelBuilder.Entity<Wallet>(eb =>
            {
                eb.Property(w => w.Pln).HasPrecision(10, 5);
                eb.Property(w => w.BitCoin).HasPrecision(10, 5);
                eb.Property(w => w.Ether).HasPrecision(10, 5);
            });
            modelBuilder.Entity<Roles>(eb =>
            {
                eb.Property(r => r.RoleName).IsRequired();

                //References
                eb.HasMany(r => r.User)
                .WithOne(u => u.Roles)
                .HasForeignKey(u => u.RolesID); 


                
            });

        }

        //Connction with data base
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
