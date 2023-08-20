using System.ComponentModel.DataAnnotations;

namespace IceProducts.Shared.InputModels
{
	public class UserInputModel
	{
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
    }
}
