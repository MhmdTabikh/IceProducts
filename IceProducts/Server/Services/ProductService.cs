using IceProducts.Server.DataContext;
using IceProducts.Server.Entities;
using IceProducts.Server.Helpers;
using IceProducts.Shared.Dtos;
using IceProducts.Shared.InputModels;
using Microsoft.EntityFrameworkCore;

namespace IceProducts.Server.Services.interfaces
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Add(ProductInputModel productInput)
        {
            var productImage = await ImageHandler.AdjustAndGetImageAsBytes(productInput.Image!);

            //add the mappers later
            var product = new Product
            {
                Image = productImage,
                LongDescription = productInput.LongDescription,
                SmallDescription = productInput.SmallDescription,
                Sizes = productInput.Sizes,
                Name = productInput.Name
            };

            await _context.Products.AddAsync(product);

            //add mappers later
            return new ProductDto
            {
                Id = product.Id,
                Image = product.Image,
                LongDescription = product.LongDescription,
                SmallDescription = product.SmallDescription,
                Name = product.Name,
                Sizes = product.Sizes
            };
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
        }

        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Image = x.Image,
                LongDescription = x.LongDescription,
                Name = x.Name,
                Sizes = x.Sizes,
                SmallDescription = x.SmallDescription
            });
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductDto> GetDtoById(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            
            if(product != null)
            {
                return new ProductDto
                {
                    Id = product.Id,
                    Image = product.Image,
                    LongDescription = product.LongDescription,
                    Name = product.Name,
                    Sizes = product.Sizes,
                    SmallDescription = product.SmallDescription
                };
            }

            return null;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        //change later not efficient (bshuf shu b3ml fiha b3den)
        public async Task Update(Product product, UpdateProductInputModel updateProductInput)
        {
            var image = await ImageHandler.AdjustAndGetImageAsBytes(updateProductInput.Image);

            product.Image = image;
            product.Name = updateProductInput.Name;
            product.SmallDescription = updateProductInput.SmallDescription;
            product.LongDescription = updateProductInput.LongDescription;
            product.Sizes = updateProductInput.Sizes;
        }
    }
}
