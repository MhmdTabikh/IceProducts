﻿using FluentValidation;
using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Http;

namespace IceProducts.Server.Validators
{
    public class ProductValidator : AbstractValidator<ProductInputModel>
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
                                            .Null()
                                            .MaximumLength(75);

            RuleFor(x => x.LongDescription).NotEmpty()
                                           .Null();

            RuleFor(x => x.Image).NotNull()
                                 .Must(ValidateFormFileType)
                                 .WithMessage("Invalid file type");

            RuleFor(x => x.Image).Must(ValidateFileSize)
                                 .WithMessage("Provided file is too large (Maximum 10MB)"); 
        }

        private bool ValidateFormFileType(IFormFile formFile)
        {
            return acceptedExtenstions.Contains(formFile.ContentType);
        }

        private bool ValidateFileSize(IFormFile formFile)
        {
            return formFile.Length <= MaxSize;
        }

    } 
}