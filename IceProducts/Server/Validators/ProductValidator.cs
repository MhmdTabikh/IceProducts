using FluentValidation;
using IceProducts.Server.Validators.ValidationModels;

namespace IceProducts.Server.Validators
{
    public class ProductValidator : AbstractValidator<ProductValidationModel> 
    {
        //~ 10MB
        private const long MaxSize = 10000000; 
        private static readonly HashSet<string> acceptedExtenstions =
        new HashSet<string> {
            "image/apng",
            "image/bmp",
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png",
            "image/svg+xml",
            "image/tiff",
            "image/webp",
            "image/x-icon"
            };

        public ProductValidator() 
        {

            RuleFor(x => x.Name).NotNull()
                                .NotEmpty();

            RuleFor(x => x.SmallDescription).NotEmpty()
                                            .NotNull()
                                            .MaximumLength(75);

            RuleFor(x => x.LongDescription).NotEmpty()
                                           .NotNull()
                                           .MaximumLength(332);

            When(x => x.ImageData != null, () =>
            {
                RuleFor(x => x.ImageData.Format).Must(ValidateFormFileType)
                                                .WithMessage("Invalid file type");

                RuleFor(x => x.ImageData.Image)
                                                .NotNull()
                                                .NotEmpty()
                                                .Must(ValidateFileSize)
                                                .WithMessage("Provided file is too large (Maximum 10MB)");
            });
        }

        private bool ValidateFormFileType(string fileType)
        {
            return acceptedExtenstions.Contains(fileType);
        }

        private bool ValidateFileSize(byte[] image)
        {
            return image.Length <= MaxSize;
        }

    } 
}
