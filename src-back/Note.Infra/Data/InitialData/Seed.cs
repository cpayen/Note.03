using Microsoft.EntityFrameworkCore;
using Note.Core.Entities;
using Note.Core.Identity;
using System;

namespace Note.Infra.Data.InitialData
{
    public static class Seed
    {
        public static void AdminAppUser(ModelBuilder modelBuilder)
        {
            var admin = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "admin@note.com",
                CreatedAt = DateTime.Now,
                CreatedBy = "Seed"
            }
            .SetPassword("Pa$$w0rd!")
            .SetRoles(new[] { UserRoles.AppAdmin, UserRoles.AppUser });

            modelBuilder.Entity<AppUser>().HasData(admin);
        }
    }
}
