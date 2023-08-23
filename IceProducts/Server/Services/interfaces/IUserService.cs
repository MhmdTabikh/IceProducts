using IceProducts.Server.Entities;
using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Services.interfaces
{
    public interface IUserService
    {
        Task<User?> IsAuthenticated(UserInputModel userInputModel);
    }
}
