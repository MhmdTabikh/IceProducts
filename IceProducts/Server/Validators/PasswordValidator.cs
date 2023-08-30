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
                                       .WithMessage("Cannot be empty")
                                       .MinimumLength(8)
                                       .WithMessage("must be at least 8 characters");

            RuleFor(x => x.NewPassword).Must(HasSpecialChars)
                                       .WithMessage("must contain at least one special character");
        }       

        private bool HasSpecialChars(string newPassword)
        {
            string specialCharacters = "!@#$%^&*()_+-=[]{};':\"|,.<>/?";
            return newPassword.Any(c => specialCharacters.IndexOf(c) != -1);
        } 

    }
}
