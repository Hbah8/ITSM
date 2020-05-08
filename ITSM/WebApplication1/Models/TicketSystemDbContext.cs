using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TicketSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Specialist> Specialists { get; set; }

        public DbSet<UserProfile> Profiles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public TicketSystemDbContext(DbContextOptions<TicketSystemDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string specialistRoleName = "specialist";

            string adminEmail = "gimatdinov.roman@gmail.com";
            string adminPassword = "123456";

            string specialistEmail = "specialist@gmail.com";
            string specialistPassword = "123456";

            //Adding roles
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role specialistRole = new Role { Id = 3, Name = specialistRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            Specialist specialistUser = new Specialist { Id = 4, Email = specialistEmail, Password = specialistPassword, RoleId = specialistRole.Id };

            modelBuilder.Entity<Role>(b =>
            {
                b.HasData(new Role[] { adminRole, userRole, specialistRole });
            });
            modelBuilder.Entity<User>(b =>
            {
                b.HasData(adminUser);
            });
            modelBuilder.Entity<Specialist>(s =>
            {
                s.HasData(specialistUser);
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
