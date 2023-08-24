using IceProducts.Server.DataContext;
using IceProducts.Server.Entities;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using Microsoft.EntityFrameworkCore;

namespace IceProducts.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly DatabaseContext _context;

        public UserService(IPasswordHasher passwordHasher, DatabaseContext context)
        {
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task<User?> Authorize(UserInputModel userInputModel)
        {
            var queryable =  await _context.Users.ToListAsync();
            return queryable.FirstOrDefault(x => x.Email == userInputModel.Email && _passwordHasher.PasswordMatches(userInputModel.Password,x.Password));
        }

        public async Task<User?> GetFirst()
        {
            return await _context.Users.FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = _passwordHasher.HashPassword(newPassword);
        }
    }
}
