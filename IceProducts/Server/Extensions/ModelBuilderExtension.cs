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
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                Email = "ta.a981111@gmail.com",
                Password = "test"
            });
        }
    }
}
