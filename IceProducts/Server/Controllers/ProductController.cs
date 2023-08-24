using IceProducts.Server.Data;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.Dtos;
using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
            //validations goes here

            var productDto = await _productService.Add(productInputModel);
            await _productService.Save(cancellationToken);
            return Ok(productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductInputModel updateProductInput, CancellationToken cancellationToken)
        {
            //validations goes here

            var product = await _productService.GetById(id); 
            if (product == null)
            {
                return BadRequest(new BaseResponse (false, $"Product with Id {id} doesn't exist"));
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
