using FluentValidation;
using IceProducts.Server.Services.interfaces;
using IceProducts.Server.Validators.ValidationModels;
using IceProducts.Shared.Dtos;
using IceProducts.Shared.InputModels;
using IceProducts.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductValidationModel> _validator;

        public ProductController(IProductService productService, IValidator<ProductValidationModel> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var productDtos = await _productService.Get();
            return Ok(productDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var productDto = await _productService.GetDtoById(id);

            if (productDto == null)
            {
                return BadRequest(new BaseResponse(false, $"Product with Id {id} doesn't exist"));
            }

            return Ok(productDto);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add([FromBody] ProductInputModel productInputModel)
        {
            // do mappings later
            var validationResult = await _validator.ValidateAsync(new ProductValidationModel
            {
                ImageData = productInputModel.ImageData,
                LongDescription = productInputModel.LongDescription,
                Name = productInputModel.Name,
                Sizes = productInputModel.Sizes,
                SmallDescription = productInputModel.SmallDescription
            });

            if(!validationResult.IsValid)
            {
                var errorMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
                return BadRequest(new BaseResponse(false, errorMessages));
            }
            var productDto = await _productService.Add(productInputModel);
            await _productService.Save();
            return Ok(productDto);  
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductInputModel updateProductInput)
        {
            var product = await _productService.GetById(id); 
            if (product == null)
            {
                return BadRequest(new BaseResponse (false, $"Product with Id {id} doesn't exist"));
            }

            var validationResult = await _validator.ValidateAsync(new ProductValidationModel
            {
                ImageData = updateProductInput.ImageData ?? null,
                LongDescription = updateProductInput.LongDescription,
                Name = updateProductInput.Name,
                Sizes = updateProductInput.Sizes,
                SmallDescription = updateProductInput.SmallDescription
            });

            if(!validationResult.IsValid)
            {
                var errorMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
                return BadRequest(new BaseResponse(false, errorMessages));
            }

            _productService.Update(product, updateProductInput);
            await _productService.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var product = await _productService.GetById(id);

            if(product == null)
            {
                return BadRequest(new BaseResponse(false, $"Product with Id {id} doesn't exist"));
            }

            _productService.Delete(product);
            await _productService.Save();
            return NoContent();
        }

        
    }
}
