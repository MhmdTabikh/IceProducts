using IceProducts.Server.Entities;
using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Services.interfaces
{
    public interface IUserService
    {
        Task<User?> Authorize(UserInputModel userInputModel);
        Task<User?> GetById(Guid id);
        Task<User?> GetFirst();
        Task Save();

        void UpdatePassword(User user, string newPassword);
    }
}
