using IceProducts.Server.Entities;
using IceProducts.Shared.Dtos;
using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Services.interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> Get();
        Task<Product?> GetById(Guid id);
        Task<ProductDto> GetDtoById(Guid id);
        Task Update(Product product, UpdateProductInputModel updateProductInput);
        Task<ProductDto> Add(ProductInputModel productInput);
        void Delete(Product product);
        Task Save(CancellationToken cancellationToken);
    }
}
