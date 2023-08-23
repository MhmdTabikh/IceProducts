﻿using IceProducts.Server.DataContext;
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

        public async Task<User?> IsAuthenticated(UserInputModel userInputModel)
        {
            return await _context.Users.Where(x => x.Email == userInputModel.Email && x.Password == userInputModel.Password).FirstOrDefaultAsync();
        }
    }
}
