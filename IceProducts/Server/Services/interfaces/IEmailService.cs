using IceProducts.Server.Emailing;
using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Services.interfaces
{
    public interface IEmailService
    {
        Task SendEmail(ContactUsInputModel contactUsInput); 
    }
}
