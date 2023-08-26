using FluentValidation;
using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Validators
{
    public class PasswordValidator : AbstractValidator<ChangePasswordInputModel>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.NewPassword).NotNull()
                                       .NotEmpty()
                                       .MinimumLength(8);

            RuleFor(x => x.NewPassword).Must(HasSpecialChars)
                                       .WithMessage("Password must contain at least one special character");
        }       

        private bool HasSpecialChars(string newPassword)
        {
            string specialCharacters = "!@#$%^&*()_+-=[]{};':\"|,.<>/?";
            return newPassword.Any(x => specialCharacters.Contains(x));
        } 

    }
}
