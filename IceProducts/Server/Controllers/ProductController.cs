using FluentValidation;
using IceProducts.Server.Data;
using IceProducts.Server.Services.interfaces;
using IceProducts.Server.Validators.ValidationModels;
using IceProducts.Shared.Dtos;
using IceProducts.Shared.InputModels;
using IceProducts.Shared.Responses;
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

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add([FromForm] ProductInputModel productInputModel, CancellationToken cancellationToken)
        {
            // do mappings later
            var validationResult = await _validator.ValidateAsync(new ProductValidationModel
            {
                Image = productInputModel.Image,
                LongDescription = productInputModel.LongDescription,
                Name = productInputModel.Name,
                Sizes = productInputModel.Sizes,
                SmallDescription = productInputModel.SmallDescription
            }, cancellationToken);

            if(!validationResult.IsValid)
            {
                var errorMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
                return BadRequest(new BaseResponse(false, errorMessages));
            }
            var productDto = await _productService.Add(productInputModel);
            await _productService.Save(cancellationToken);
            return Ok(productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductInputModel updateProductInput, CancellationToken cancellationToken)
        {
            var product = await _productService.GetById(id); 
            if (product == null)
            {
                return BadRequest(new BaseResponse (false, $"Product with Id {id} doesn't exist"));
            }

            // do mappings later
            var validationResult = await _validator.ValidateAsync(new ProductValidationModel
            {
                Image = updateProductInput.Image,
                LongDescription = updateProductInput.LongDescription,
                Name = updateProductInput.Name,
                Sizes = updateProductInput.Sizes,
                SmallDescription = updateProductInput.SmallDescription
            },cancellationToken);

            if (!validationResult.IsValid) 
            {
                var errorMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
                return BadRequest(new BaseResponse(false, errorMessages));
            }
            await _productService.Update(product, updateProductInput);
            await _productService.Save(cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetById(id);

            if(product == null)
            {
                return BadRequest(new BaseResponse(false, $"Product with Id {id} doesn't exist"));
            }

            _productService.Delete(product);
            await _productService.Save(cancellationToken);
            return NoContent();
        }

        
    }
}
