using IceProducts.Server.Entities;
using IceProducts.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace IceProducts.Server.Extensions
{
    public static class ModelBuilderExtension
    {
        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            PasswordHasher hasher = new PasswordHasher();
            string email = "ta.a981111@gmail.ccom";
            string password = "admin";

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = hasher.HashPassword(password)
            });
        }
    }
}
